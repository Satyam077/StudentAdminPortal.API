using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Validators
{
    //public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequestValidator>
    //{
        //public UpdateStudentRequestValidator(IStudentRepository studentRepository)
        //{
            //RuleFor(x => x.Email).NotEmpty().EmailAddress();
            //RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(10000000000);
            //RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            //{
            //    var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == id);
            //    if (gender != null)
            //    {
            //        return true;
            //    }
            //    return false;
            //}).WithMessage("Plz select valid Gender!!");
    //    }
    //}
}
