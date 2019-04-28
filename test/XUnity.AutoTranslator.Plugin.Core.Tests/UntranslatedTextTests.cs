﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnity.AutoTranslator.Plugin.Core.Extensions;

namespace XUnity.AutoTranslator.Plugin.Core.Tests
{
   public class UntranslatedTextTests
   {
      [Theory( DisplayName = "Can_Trim_Surrounding_Whitespace" )]
      [InlineData( "\r\n \r\nHello", "Hello", "\r\n \r\n", null )]
      [InlineData( "\r\n \r\nHello\n", "Hello", "\r\n \r\n", "\n" )]
      [InlineData( "\r\r\r\r\n \n　Hello  \r\n", "Hello", "\r\r\r\r\n \n　", "  \r\n" )]
      public void Can_Trim_Surrounding_Whitespace( string input, string expectedTrimmedText, string expectedLeadingWhitespace, string expectedTrailingWhitespace )
      {
         var untranslatedText = new UntranslatedText( input, false, false );

         Assert.Equal( input, untranslatedText.TranslatableText );
         Assert.Equal( expectedTrimmedText, untranslatedText.TrimmedText );
         Assert.Equal( expectedLeadingWhitespace, untranslatedText.LeadingWhitespace );
         Assert.Equal( expectedTrailingWhitespace, untranslatedText.TrailingWhitespace );
      }

      [Theory( DisplayName = "Can_Trim_Internal_Whitespace" )]
      [InlineData( "Hel lo", "Hello" )]
      [InlineData( "Hel\r\n lo", "Hello" )]
      public void Can_Trim_Internal_Whitespace( string input, string expectedTrimmedText )
      {
         var untranslatedText = new UntranslatedText( input, false, true );
         
         Assert.Equal( expectedTrimmedText, untranslatedText.TrimmedText );
      }

      [Theory( DisplayName = "Can_Trim_Internal_And_Surrounding_Whitespace" )]
      [InlineData( "\r\n \r\nHe llo", "Hello", "\r\n \r\n", null )]
      [InlineData( "\r\n \r\nHel\r\nlo\n", "Hello", "\r\n \r\n", "\n" )]
      [InlineData( "\r\r\r\r\n \n　Hell\no  \r\n", "Hello", "\r\r\r\r\n \n　", "  \r\n" )]
      public void Can_Trim_Internal_And_Surrounding_Whitespace( string input, string expectedTrimmedText, string expectedLeadingWhitespace, string expectedTrailingWhitespace )
      {
         var untranslatedText = new UntranslatedText( input, false, true );

         Assert.Equal( expectedTrimmedText, untranslatedText.TrimmedText );
         Assert.Equal( expectedLeadingWhitespace, untranslatedText.LeadingWhitespace );
         Assert.Equal( expectedTrailingWhitespace, untranslatedText.TrailingWhitespace );
      }

      [Theory( DisplayName = "Can_Trim_Internal_And_Surrounding_Whitespace_And_Template" )]
      [InlineData( "\r\n \r\nFPS: 60.53", "FPS:{{A}}", "\r\n \r\n", null )]
      public void Can_Trim_Internal_And_Surrounding_Whitespace_And_Template( string input, string expectedTrimmedText, string expectedLeadingWhitespace, string expectedTrailingWhitespace )
      {
         var untranslatedText = new UntranslatedText( input, true, true );

         Assert.Equal( expectedTrimmedText, untranslatedText.TrimmedText );
         Assert.Equal( expectedLeadingWhitespace, untranslatedText.LeadingWhitespace );
         Assert.Equal( expectedTrailingWhitespace, untranslatedText.TrailingWhitespace );
      }
   }
}
