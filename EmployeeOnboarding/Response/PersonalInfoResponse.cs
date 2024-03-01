using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Response
{
    public class OverallPersonalInfoResponse
    {
        public PersonalInfoResponse result { get; set; }

    }
    public class PersonalInfoResponse
    {
        public int loginId { get; set; }
        public int GenId { get; set; }
        public GeneralInfoResponse generalVM { get; set; }
        public List<ContactResponse> contact { get; set; }
        public List<FamilyResponse> families { get; set; }
        public HobbyResponse hobby { get; set; }
        public List<ColleagueResponse> colleagues { get; set; }
        public List<EmergencyContactResponse> emergencies { get; set; }
        public RequiredDocumentsRespose RequiredDocuments { get; set; }
    }

    public class GeneralInfoResponse
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public string Empname { get; set; }
        public string Personal_Emailid { get; set; }
        public long Contact_no { get; set; }
        public DateOnly DOB { get; set; }
        public string Nationality { get; set; }
        public int Gender { get; set; }
        public int? MaritalStatus { get; set; }
        public DateOnly? DateOfMarriage { get; set; }
        public int BloodGrp { get; set; }
        public byte[] Profile_Pic { get; set; }
    }
    public class ContactResponse
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Country_Id { get; set; }
        public int State_Id { get; set; }
        public int City_Id { get; set; }
        public string Pincode { get; set; }
        public string AddressType { get; set; }
    }

    public class FamilyResponse
    {
        public string Relationship { get; set; }
        public string Name { get; set; }
        public DateOnly DOB { get; set; }
        public string Occupation { get; set; }
        public long contact { get; set; }
    }
    public class HobbyResponse
    {
        public bool ProfessionalBody { get; set; }
        public string? ProfessionalBody_name { get; set; }
        public string? Hobbies { get; set; }
    }
    public class ColleagueResponse
    {
        public string? Empid { get; set; }
        public string? Colleague_Name { get; set; }
        public string? Location { get; set; }
    }
    public class EmergencyContactResponse
    {

        public string Relationship { get; set; }
        public string Relation_name { get; set; }
        public long Contact_number { get; set; }
        public string Contact_address { get; set; }
    }
    public class RequiredDocumentsRespose
    {
        public byte[] Aadhar { get; set; }
        public byte[] Pan { get; set; }
        public byte[]? Driving_license { get; set; }
        public byte[]? Passport { get; set; }
    }
}

