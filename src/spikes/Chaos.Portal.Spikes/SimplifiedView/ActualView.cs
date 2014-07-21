namespace Chaos.Portal.Spikes.SimplifiedView
{
    using System;
    using System.Collections.Generic;

    class ActualView : IView
    {
        public void Index(object value, ICacheWriter cache, ISearchWriter searcher)
        {
            var input = value as InputData;

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
            searcher.AddDocument(doc);

            // create cachable dto
            cache.AddDocument(new CacheDocument<ActualViewData>("Namespace.Class", viewData));
            cache.AddDocument(new CacheDocument<object>("Namespace.Class", input));
        }

        internal class ActualViewData
        {
            public string Name { get; set; }
            public uint Number { get; set; }
            public uint Type { get; set; }
        }
    }

    internal class InputData
    {
        public uint Type { get; set; }

        public string Name { get; set; }

        public uint Number { get; set; }
    }

    public interface ICacheDocument<out TDtoType>
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

    internal class CacheWriter : ICacheWriter
    {
        public void AddDocument<T>(ICacheDocument<T> dto)
        {
            Console.WriteLine(dto.Fullname);
            Console.WriteLine(dto.Dto.ToString());
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }

    internal class IndexWriter : ISearchWriter
    {
        public void AddDocument(IDictionary<string, string> document)
        {

        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
