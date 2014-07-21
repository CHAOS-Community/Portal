namespace Chaos.Portal.Spikes.SimplifiedView
{
    using System;
    using System.Collections.Generic;

    public class ViewInfo
    {
        public ICacheWriter Cache { get; set; }
        public ISearchWriter Searcher { get; set; }
        public Func<IView> ViewFactory { get; set; }
    }

    public interface ISearchWriter : IBufferedWriter
    {
        void AddDocument(IDictionary<string, string> document);
    }

    public interface ICacheWriter : IBufferedWriter
    {
        void AddDocument<T>(ICacheDocument<T> dto);
    }

    public interface IBufferedWriter
    {
        void Commit();
    }

    public interface IView
    {
        void Index(object value, ICacheWriter cache, ISearchWriter searcher);
    }
}
