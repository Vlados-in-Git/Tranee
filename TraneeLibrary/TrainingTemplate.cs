using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraneeLibrary
{
    public class TrainingTemplate
    {
        public int Id { get; set; }

        public string Name { get; set; }                    // name of training 
        public int RestBetweenSets { get; set; }
        public string? Description { get; set; }

        public List<ExerciseTemplate> ExerciseTemplates { get; set; } = new List<ExerciseTemplate>();
    }

    public class ExerciseTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupOfMuscle { get; set; }

        public int RestBetweenSets { get; set; }
        public int TargetSets { get; set; }
        public int TargetReps { get; set; }

        public int TrainingTemplateId { get; set; }
        public TrainingTemplate TrainingTemplate { get; set; } = null!;


    }

}
