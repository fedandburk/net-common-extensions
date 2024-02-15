# Extensions for .NET
![GitHub](https://img.shields.io/github/license/fedandburk/net-common-extensions.svg)
![Nuget](https://img.shields.io/nuget/v/Fedandburk.Common.Extensions.svg)
[![CI](https://github.com/fedandburk/net-common-extensions/actions/workflows/ci.yml/badge.svg)](https://github.com/fedandburk/net-common-extensions/actions/workflows/ci.yml)
[![CD](https://github.com/fedandburk/net-common-extensions/actions/workflows/cd.yml/badge.svg)](https://github.com/fedandburk/net-common-extensions/actions/workflows/cd.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/fedandburk/net-common-extensions/badge)](https://www.codefactor.io/repository/github/fedandburk/net-common-extensions)

## Installation

Use [NuGet](https://www.nuget.org) package manager to install this library.

```bash
Install-Package Fedandburk.Common.Extensions
```

## Usage
```cs
using Fedandburk.Common.Extensions;
```

### ICommand Extensions
To safely check whether the `ICommand` can be executed in its current state:

```cs
var canExecute = Command.SafeCanExecute(parameter);
```

To safely invoke the `ICommand` if this command can be executed in its current state:

```cs
var isExecuted = Command.SafeExecute(parameter);
```

### IEnumerable Extensions
To search for the specified object and get the index of its first occurrence in a collection:

```cs
var index = default(IEnumerable).IndexOf(item);
var index = default(IEnumerable).IndexOf(item, comparer);
var index = default(IEnumerable<TItem>).IndexOf(item);
var index = default(IEnumerable<<TItem>>).IndexOf(item, comparer);
```

To get a specified number of contiguous elements from the specified index:

```cs
var items = default(IEnumerable<TItem>).Take(startIndex, length);
```

To check if the specified enumerable is `null` or has a length of zero:

```cs
var isNullOrEmpty = default(IEnumerable).IsNullOrEmpty();
```

To get the number of elements in a sequence:

```cs
var count = default(IEnumerable).Count();
```

To compute the sum of a sequence of TimeSpan values:

```cs
var sum = new[] {100L, 1000L, 10000L}.Sum(TimeSpan.FromTicks);
var sum = new[] {TimeSpan.FromDays(1), TimeSpan.FromHours(2)}.Sum();
```

### String Extensions
To check is the specified string is null or an empty string:

```cs
var isNullOrEmpty = "string".IsNullOrEmpty();
```

To check is the specified string is null, empty, or consists only of white-space characters:

```cs
var isNullOrWhiteSpace = "string".IsNullOrWhiteSpace();
```
