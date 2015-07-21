﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PatioBlox2016.Concrete
{
  using System;

	public class Description
	{
    public static readonly Regex SizeRegex = 
      new Regex(@"(\d+\.?\d*)-?(IN|SQ ?FT)?-? ?(X)? ?(H(?= ))? ?", RegexOptions.Compiled);
    
    private Description()
		{
		}

		public Description(int jobId, string text) : this()
    {
      if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

		  Id = 0;
      JobId = jobId;
      Text = text;
		  Size = ExtractSize();
    }

    /// <summary>
    /// Removes the Size component from the Text and returns the remainder.
    /// </summary>
    /// <returns></returns>
    public string ExtractRemainder()
    {
      return SizeRegex.Replace(Text, string.Empty);
    }

    private string ExtractSize()
    {
      var matchList = new List<string>();
      var matches = SizeRegex.Matches(Text);

      for (var i = 0; i < matches.Count; i++) {
        matchList.Add(matches[i].Value.ToUpper().Trim(' ', 'X'));
      }

      return string.Join(" x ", matchList);
    }

		public int Id { get; private set; }
    public int JobId { get; private set; }
    public string Text { get; private set; }

    public string Vendor { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }

    protected bool Equals(Description other)
    {
      return string.Equals(Text, other.Text);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Description) obj);
    }

    public override int GetHashCode()
    {
      return Text.GetHashCode();
    }

    public static bool operator ==(Description left, Description right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Description left, Description right)
    {
      return !Equals(left, right);
    }
  }
}