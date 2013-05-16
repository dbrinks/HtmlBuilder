HtmlBuilder
===========

HtmlBuilder is an alternative to TagBuilder that allows you to use CSS selectors in order to generate complex, well-formed HTML.

The syntax is modeled after [Emmet](http://emmet.io/) (formerly Zencoding), so if you use either of those tools when writing HTML, HtmlBuilder should feel very familiar to you.

##Features

* Easily create complex html using a modified CSS selector syntax modeled after [Emmet](http://emmet.io/).
* More in development!

##Usage

####Simple Element

Creating a simple element using HtmlBuilder is as simple as passing your desired tag name to the constructor.

```csharp
var html = new HtmlBuilder("div");

// <div></div>
```

HtmlBuilder requires that some form of selector be passed in.  `null` or `String.Empty` will cause HtmlBuilder to thow an exception.

When creating an element using a complex selector (anything beyond a tag name), HtmlBuilder will assume you're creating a `div`, unless stated otherwise in the selector.

####Element with an ID

```csharp
var html = new HtmlBuilder("#foo");

// <div id="foo"></div>
```

####Element with a class

```csharp
var html = new HtmlBuilder(".bar");

// <div class="bar"></div>
```

####Element with an attribute

```csharp
var but = new HtmlBuilder("button[disabled]");

// <button disabled=""></button>

var but2 = new HtmlBuilder("button[disabled=\"disabled\""]);

// <button disabled="disabled"></button>
```

####Element with a sibling

```csharp
var html = new HtmlBuilder("a+span");

// <a></a><span></span>
```

####ElementWith a child

```csharp
var list = new HtmlBuilder("ul>li");

// <ul><li></li></ul>
```

####Multiple elements

```csharp
var html = new HtmlBuilder("div*3");

// <div></div><div></div><div></div>
```

####Complex selector

Throw it all together...

```csharp
var list = new HtmlBuilder("ul#my-list>li.list-item*5>a[href]");

/* Formatted this so it's easier to read.

<ul id="my-list">
    <li class="list-item"><a href=""></a></li>
    <li class="list-item"><a href=""></a></li>
    <li class="list-item"><a href=""></a></li>
    <li class="list-item"><a href=""></a></li>
    <li class="list-item"><a href=""></a></li>
</ul>
*/
```

##Fluent API

Still in development

##Future

HtmlBuilder is still in development.  Here are a few things I am still working on.

* Expand on the current API.
* Support text in a selector by writing `{Some Words}`.
* Support grouping elements by using parenthesis `(label+input)*2`.
* Easy to read debug HTML output with new lines and decent formatting.
* Traverse the HTML tree created by your selector (break all the things).