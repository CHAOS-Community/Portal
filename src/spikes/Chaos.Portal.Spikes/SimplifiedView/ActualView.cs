namespace Chaos.Portal.Spikes.SimplifiedView
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    class ActualView
    {
        public void Index(object obj)
        {
            var indexWriter = new IndexWriter();
            var cacheWriter = new CacheWriter();
            dynamic input = new ExpandoObject();
            input.Name = "some name";
            input.Number = 1u;
            input.Type = 1u;

            // trigger
            if (input.Type != 1u) return;

            // get data from external sources
            var externalVal = "external!";

            // map to common format
            var viewData = new ActualViewData
                {
                    Name = input.Name + " " + externalVal,
                    Number = input.Number,
                    Type = input.Type
                };

            // create index document
            var doc = new Dictionary<string, string>
                {
                    {"Name", input.Name},
                    {"Number", input.Number.ToString()},
                    {"Type", input.Type.ToString()}
                };
            indexWriter.AddDocument(doc);

            // create cachable dto
            cacheWriter.AddDocument(new CacheDocument<ActualViewData>("Namespace.Class", viewData));
            cacheWriter.AddDocument(new CacheDocument<object>("Namespace.Class", input));
        }

        internal class ActualViewData
        {
            public string Name { get; set; }
            public uint Number { get; set; }
            public uint Type { get; set; }
        }
    }

    internal interface ICacheDocument<out TDtoType>
    {
        string Fullname { get; }
        TDtoType Dto { get; }
    }

    internal class CacheDocument<TDtoType> : ICacheDocument<TDtoType>
    {
        public string Fullname { get; set; }
        public TDtoType Dto { get; set; }

        public CacheDocument(string fullname, TDtoType dto)
        {
            Fullname = fullname;
            Dto = dto;
        }
    }

    internal class CacheWriter
    {
        public void AddDocument<T>(ICacheDocument<T> dto)
        {
            Console.WriteLine(dto.Fullname);
            Console.WriteLine(dto.Dto.ToString());
        }
    }

    internal class IndexWriter
    {
        public void AddDocument(IDictionary<string, string> document)
        {

        }
    }
}
