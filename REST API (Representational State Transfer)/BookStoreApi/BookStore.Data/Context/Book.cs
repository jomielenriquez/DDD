using System;
using System.Collections.Generic;

namespace BookStore.Data.Context;

public partial class Book
{
    public Guid BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;
}
