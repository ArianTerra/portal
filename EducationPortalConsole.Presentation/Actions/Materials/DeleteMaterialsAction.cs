using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials
{
    public class DeleteMaterialsAction : Action
    {
        public DeleteMaterialsAction()
        {
            Name = "Delete Materials";
        }

        public override void Run()
        {
            base.Run();

            IMaterialService materialService = Configuration.Instance.MaterialService;
            ICourseService courseService = Configuration.Instance.CourseService;
            
            var materials = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Material>()
                    .Title("Delete selected [green]materials[/]")
                    .NotRequired()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a material, " + 
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoices(materialService.GetAll())
                    .UseConverter(x => x.Name)
                );

            foreach (var material in materials)
            {
                var skip = false;
                foreach (var course in courseService.GetAll())
                {
                    if (course.Materials.Contains(material))
                    {
                        AnsiConsole.Write(
                            new Markup($"Cannot delete material with ID {material.Id} because it used in " +
                                       $"course with ID {course.Id}\n"));
                        skip = true;
                    }
                }
                
                if(skip) continue;

                materialService.Delete(material);
            }
            
            AnsiConsole.Write(new Markup($"Materials deleted\n"));
            
            WaitForUserInput();
            Back();
        }
    }
}