//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeGaService
{
    using System;
    using System.Collections.Generic;
    
    public partial class player
    {
        public player()
        {
            this.game_words_history = new HashSet<game_words_history>();
            this.games = new HashSet<game>();
            this.games1 = new HashSet<game>();
        }
    
        public int id { get; set; }
        public string nickname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    
        public virtual ICollection<game_words_history> game_words_history { get; set; }
        public virtual ICollection<game> games { get; set; }
        public virtual ICollection<game> games1 { get; set; }
    }
}
