using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StackOverflowClone.DomainModels
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteID { get; set; }
        public int UserID { get; set; }
        public int AnswerID { get; set; }
        public int VoteValue { get; set; }


        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("AnswerID")]
        public virtual Answer Answer { get; set; }
    }
}
