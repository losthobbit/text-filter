TextFilter.sln
├── src/
│   └── TextFilter.App/
│       ├── Program.cs                  (DI wiring, reads args, writes output)
│       ├── IO/
│       │   ├── ITextReader.cs
│       │   └── FileTextReader.cs
│       ├── Filters/
│       │   ├── ITextFilter.cs
│       │   ├── VowelInMiddleFilter.cs
│       │   ├── ShortWordFilter.cs
│       │   └── ContainsTFilter.cs
│       └── Pipeline/
│           ├── IFilterPipeline.cs
│           └── FilterPipeline.cs
└── tests/
    └── TextFilter.Tests/
        ├── Filters/
        │   ├── FilterBaseTests.cs
        │   ├── VowelInMiddleFilterTests.cs
        │   ├── ShortWordFilterTests.cs
        │   └── ContainsTFilterTests.cs
        ├── Pipeline/
        │   └── FilterPipelineTests.cs
        └── IO/
            └── FileTextReaderTests.cs