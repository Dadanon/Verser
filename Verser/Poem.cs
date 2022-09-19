using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Verser
{
    internal class Poem
    {
        [Key]
        public string Text { get; set; }
        public string Title {get; set; }
        public string Author { get; set; }
        

        public Poem()
        {

        }

        public Poem(string Title, string Author, string Text)
        {
            this.Title = Title;
            this.Author = Author;
            this.Text = Text;
        }
    }
}
