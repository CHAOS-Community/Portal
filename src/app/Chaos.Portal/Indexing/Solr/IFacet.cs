﻿namespace Chaos.Portal.Indexing.Solr
{
    public interface IFacet
    {
        string DataType { get; set; }
        string Value { get; set; }
        uint Count { get; set; }
    }
}