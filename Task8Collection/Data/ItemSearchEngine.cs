using System.Text;
using Lucene.Net.Analysis.Ru;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Identity;
using Task8Collection.Data.Entities;
using Field = Task8Collection.Data.Entities.Field;

namespace Task8Collection.Data;

public class ItemSearchEngine : ISearchEngine<Item>
{
    private readonly StandardAnalyzer _analyzer;
    private readonly RAMDirectory _directory;
    private readonly IndexWriter _writer;
    private const LuceneVersion _version = LuceneVersion.LUCENE_48;
    public ItemSearchEngine()
    {
        _analyzer = new StandardAnalyzer(_version);
        _directory = new RAMDirectory();
        var config = new IndexWriterConfig(_version, _analyzer);
        _writer = new IndexWriter(_directory, config);
    }

    public void AddItemsToIndex(IEnumerable<Item> items)
    {
        foreach (var item in items)
        {
            var document = new Document();
            document.Add(new Lucene.Net.Documents.StringField("Id", item.Id.ToString(), Lucene.Net.Documents.Field.Store.YES));
            document.Add(new TextField("Name", item.Name, Lucene.Net.Documents.Field.Store.YES));
            document.Add(new TextField("CollectionId", item.Collection.Id.ToString(), Lucene.Net.Documents.Field.Store.YES));
            document.Add(new TextField("CollectionName", item.Collection.Name, Lucene.Net.Documents.Field.Store.YES));
            foreach (var field in item.Fields)
            {
                document.Add(new TextField("Fields", field.ToString(), Lucene.Net.Documents.Field.Store.YES));
            }
            foreach (var tag in item.Tags)
            {
                document.Add(new TextField("Tag", tag.Name, Lucene.Net.Documents.Field.Store.YES));
            }
            _writer.AddDocument(document);
        }
        _writer.Commit();
    }

    public IEnumerable<Item> Search(string searchTerm)
    {
        var directoryReader = DirectoryReader.Open(_directory);
        var indexSearcher = new IndexSearcher(directoryReader);

        string[] fields = { "Name", "CollectionName", "Fields", "Tag" };
        var queryParser = new MultiFieldQueryParser(_version, fields, _analyzer);
        var query = queryParser.Parse(searchTerm);

        var hits = indexSearcher.Search(query, 10).ScoreDocs;
        var items = new List<Item>();
        
        foreach (var hit in hits)
        {
            var document = indexSearcher.Doc(hit.Doc);
            items.Add(new Item()
            {
                Id = new Guid(document.Get("Id")),
                Name = document.Get("Name"),
                Collection = new Collection()
                {
                    Id = new Guid(document.Get("CollectionId")),
                    Name = document.Get("CollectionName")
                }
            });
        }

        return items;
    }
}