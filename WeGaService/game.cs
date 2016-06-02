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
    
    public partial class game
    {
        public game()
        {
            this.game_letters_history = new HashSet<game_letters_history>();
            this.game_words_history = new HashSet<game_words_history>();
        }
    
        public int id { get; set; }
        public int player1_id { get; set; }
        public int player2_id { get; set; }
        public Nullable<int> winner { get; set; }
        public int player1_score { get; set; }
        public Nullable<int> player2_score { get; set; }
        public System.DateTime date_started { get; set; }
        public Nullable<System.DateTime> date_ended { get; set; }
        public bool game_neglected { get; set; }
    
        public virtual ICollection<game_letters_history> game_letters_history { get; set; }
        public virtual ICollection<game_words_history> game_words_history { get; set; }
        public virtual player player { get; set; }
        public virtual player player1 { get; set; }
    }
}
