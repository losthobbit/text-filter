# Text Filter — Build Instructions

## Prerequisites

- Visual Studio 2022 or later (with .NET 10 SDK)
- .NET 10 SDK
- Git

---

## 1. Scaffold the Solution

Open a terminal in `E:\dev\coding-tests\text-filter` and run:

```bash
# Create the solution file
dotnet new sln -n TextFilter

# Create the src project (console app)
mkdir src
dotnet new console -n TextFilter.App -o src/TextFilter.App --framework net10.0

# Create the test project
mkdir tests
dotnet new xunit -n TextFilter.Tests -o tests/TextFilter.Tests --framework net10.0

# Add both projects to the solution
dotnet sln TextFilter.sln add src/TextFilter.App/TextFilter.App.csproj
dotnet sln TextFilter.sln add tests/TextFilter.Tests/TextFilter.Tests.csproj

# Add a project reference from Tests → App
dotnet add tests/TextFilter.Tests/TextFilter.Tests.csproj reference src/TextFilter.App/TextFilter.App.csproj
```

---

## 2. Add NuGet Packages

```bash
# Test project dependencies
dotnet add tests/TextFilter.Tests/TextFilter.Tests.csproj package NSubstitute
dotnet add tests/TextFilter.Tests/TextFilter.Tests.csproj package Shouldly
dotnet add tests/TextFilter.Tests/TextFilter.Tests.csproj package coverlet.collector

# App project dependencies
dotnet add src/TextFilter.App/TextFilter.App.csproj package Microsoft.Extensions.DependencyInjection
```

---

## 3. Create the Folder Structure

```bash
# App folders
mkdir src/TextFilter.App/IO
mkdir src/TextFilter.App/Filters
mkdir src/TextFilter.App/Pipeline

# Test folders
mkdir tests/TextFilter.Tests/IO
mkdir tests/TextFilter.Tests/Filters
mkdir tests/TextFilter.Tests/Pipeline
```

The final structure should look like this:

```
TextFilter.sln
├── src/
│   └── TextFilter.App/
│       ├── Program.cs
│       ├── IO/
│       │   ├── ITextReader.cs
│       │   └── FileTextReader.cs
│       ├── Filters/
│       │   ├── ITextFilter.cs
│       │   ├── FilterBase.cs
│       │   ├── VowelInMiddleFilter.cs
│       │   ├── ShortWordFilter.cs
│       │   └── ContainsTFilter.cs
│       └── Pipeline/
│           ├── IFilterPipeline.cs
│           └── FilterPipeline.cs
└── tests/
    └── TextFilter.Tests/
        ├── IO/
        │   └── FileTextReaderTests.cs
        ├── Filters/
        │   ├── VowelInMiddleFilterTests.cs
        │   ├── ShortWordFilterTests.cs
        │   └── ContainsTFilterTests.cs
        └── Pipeline/
            └── FilterPipelineTests.cs
```

---

## 4. Open in Visual Studio

Open `TextFilter.sln` in Visual Studio. From this point all work is done TDD — tests first, then implementation.

---

## 5. TDD Build Order

Work through the files in this exact order. Write the test file first, watch it fail, then write the implementation to make it pass.

### Step 1 — Interfaces and abstractions (no logic, just contracts)

Create these files with empty/stub implementations so the solution compiles:

- `IO/ITextReader.cs`
- `Filters/ITextFilter.cs`
- `Filters/FilterBase.cs` (abstract class implementing `ITextFilter`, no logic yet)
- `Pipeline/IFilterPipeline.cs`

These have no logic yet — just enough to compile.

> Note: `FilterBase` is not tested directly. It will emerge naturally during the
> refactoring step after the first two filters are implemented and duplication
> is observed. This is the correct TDD flow — the base class is extracted from
> working code, not designed upfront.

---

### Step 2 — VowelInMiddleFilter (Filter 1)

**Write tests first:** `tests/TextFilter.Tests/Filters/VowelInMiddleFilterTests.cs`

Test cases to cover:

- Odd-length word: middle letter IS a vowel → removed (e.g. `"clean"` → middle `'e'`)
- Odd-length word: middle letter is NOT a vowel → kept (e.g. `"the"` → middle `'h'`)
- Even-length word: first of two middle letters IS a vowel → removed
- Even-length word: second of two middle letters IS a vowel → removed (e.g. `"what"` → middle `'ha'`, `'a'` is a vowel)
- Even-length word: neither middle letter is a vowel → kept (e.g. `"rather"` → middle `'th'`)
- Case-insensitive vowel check (e.g. `"CLEAN"` is still removed)
- Single character word — not removed by this filter
- Two character word — both chars are the middle, removed if either is a vowel
- Word from the brief: `"currently"` → middle `'e'` → removed
- Word from the brief: `"rather"` → middle `'th'` → kept

**Then implement:** `src/TextFilter.App/Filters/VowelInMiddleFilter.cs`

---

### Step 3 — ShortWordFilter (Filter 2)

**Write tests first:** `tests/TextFilter.Tests/Filters/ShortWordFilterTests.cs`

Test cases to cover:

- Word of length 1 → removed
- Word of length 2 → removed
- Word of length 3 → kept
- Word of length 4+ → kept
- Empty string → returns empty string
- Numbers treated as words (e.g. `"12"` → removed, `"123"` → kept)

**Then implement:** `src/TextFilter.App/Filters/ShortWordFilter.cs`

> At this point, review `VowelInMiddleFilter` and `ShortWordFilter` for shared
> logic. If both use the same word-iteration and removal mechanism, extract it
> into `FilterBase` now. All existing tests should remain green after the
> refactor.

---

### Step 4 — ContainsTFilter (Filter 3)

**Write tests first:** `tests/TextFilter.Tests/Filters/ContainsTFilterTests.cs`

Test cases to cover:

- Word containing lowercase `'t'` → removed
- Word containing uppercase `'T'` → removed
- Word starting with `'t'` → removed
- Word ending with `'t'` → removed
- Word with no `'t'` → kept
- Empty string → returns empty string

**Then implement:** `src/TextFilter.App/Filters/ContainsTFilter.cs`

---

### Step 5 — FilterPipeline

**Write tests first:** `tests/TextFilter.Tests/Pipeline/FilterPipelineTests.cs`

Use NSubstitute to mock `ITextFilter`.

Test cases to cover:

- Single filter: input is passed to the filter, output is returned
- Two filters: output of first filter is passed as input to second
- Three filters: sequential chaining is correct
- Empty filter list: input is returned unchanged
- Filters are applied in the order they are provided

**Then implement:** `src/TextFilter.App/Pipeline/FilterPipeline.cs`

---

### Step 6 — FileTextReader

**Write tests first:** `tests/TextFilter.Tests/IO/FileTextReaderTests.cs`

> No mocking needed here — use a real temp file.

Test cases to cover:

- File with content returns that content as a string
- Use `Path.GetTempFileName()` to create the temp file, and clean it up in
  test teardown (`IDisposable` or `Dispose()`)

**Then implement:** `src/TextFilter.App/IO/FileTextReader.cs`

---

### Step 7 — Wire up Program.cs

No tests for `Program.cs` — it is pure DI wiring and a single `Console.WriteLine`.

Register each type explicitly and construct the pipeline with a defined order:

```csharp
services.AddTransient<VowelInMiddleFilter>();
services.AddTransient<ShortWordFilter>();
services.AddTransient<ContainsTFilter>();
services.AddTransient<ITextReader, FileTextReader>();
services.AddTransient<IFilterPipeline>(sp => new FilterPipeline(new ITextFilter[]
{
    sp.GetRequiredService<VowelInMiddleFilter>(),
    sp.GetRequiredService<ShortWordFilter>(),
    sp.GetRequiredService<ContainsTFilter>()
}));
```

> Filter ordering is intentionally explicit here rather than relying on
> registration order. This makes the pipeline sequence visible and safe to
> change without side effects.

`Program.cs` responsibilities:

1. Read `args[0]` as the file path
2. Build and configure the DI container
3. Resolve `ITextReader`, read the file
4. Resolve `IFilterPipeline`, apply filters
5. `Console.WriteLine` the result

---

## 6. Add the Input File

Create the input text file:

```bash
mkdir src/TextFilter.App/Resources
```

Create `src/TextFilter.App/Resources/alice.txt` and paste the provided Alice in
Wonderland excerpt into it.

In `TextFilter.App.csproj`, ensure the file is copied to output:

```xml
<ItemGroup>
  <Content Include="Resources/alice.txt">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

---

## 7. Add Documentation Files

Place these in the `docs/` folder (which already exists):

- `docs/ASSUMPTIONS.md` — copy from the assumptions document already created

Place this in the repo root:

- `ASSUMPTIONS.md` — same file, root copy for visibility on GitHub

---

## 8. Run the Tests

```bash
dotnet test tests/TextFilter.Tests/TextFilter.Tests.csproj
```

All tests should pass with 100% coverage of the `Filters/`, `Pipeline/`, and
`IO/` namespaces.

To verify coverage:

```bash
dotnet test tests/TextFilter.Tests/TextFilter.Tests.csproj --collect:"XPlat Code Coverage"
```

---

## 9. Run the Application

```bash
dotnet run --project src/TextFilter.App/TextFilter.App.csproj -- "src/TextFilter.App/Resources/alice.txt"
```

---

## 10. Commit and Push

```bash
git add .
git commit -m "Initial implementation of TextFilter with TDD"
git push origin main
```
