using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021638)]
    public class AddCitySeeding_202308021638 : Migration
    {
     
          public override void Down()
        {
            for (int stateId = 1; stateId <= 36; stateId++)
            {
                Delete.FromTable("State").Row(new { State_Id = stateId });
            }
        }

    

        public override void Up()
        {
        

              //1 
            Insert.IntoTable("City").Row(new { Id = "1", City_Name = "Anantapur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") }); 
            Insert.IntoTable("City").Row(new { Id = "2", City_Name = "Chittoor", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "3", City_Name = "East Godavari", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "4", City_Name = "Guntur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "5", City_Name = "Krishna", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "6", City_Name = "Kurnool", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "7", City_Name = "Nellore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "8", City_Name = "Prakasam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "9", City_Name = "Srikakulam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "10", City_Name = "Visakhapatnam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "11", City_Name = "Vizianagaram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "12", City_Name = "West Godavari", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            Insert.IntoTable("City").Row(new { Id = "13", City_Name = "YSR Kadapa", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("1") });
            //2
            Insert.IntoTable("City").Row(new { Id = "14", City_Name = "Tawang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "15", City_Name = "West Kameng", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "16", City_Name = "East Kameng", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "17", City_Name = "Papum Pare", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "18", City_Name = "Kurung Kumey", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "19", City_Name = "Kra Daadi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "20", City_Name = "Lower Subansiri", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "21", City_Name = "Upper Subansiri", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "22", City_Name = "West Siang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "23", City_Name = "East Siang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "24", City_Name = "Siang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "25", City_Name = "Upper Siang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "26", City_Name = "Lower Siang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "27", City_Name = "Dibang Valley", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "28", City_Name = "Lower Dibang Valley", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "29", City_Name = "Anjaw", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "30", City_Name = "Lohit", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "31", City_Name = "Namsai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "32", City_Name = "Changlang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "33", City_Name = "Tirap", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });
            Insert.IntoTable("City").Row(new { Id = "34", City_Name = "Longding", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("2") });

            //3
            Insert.IntoTable("City").Row(new { Id = "35", City_Name = "Baksa", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "36", City_Name = "Barpeta", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "37", City_Name = "Biswanath", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "38", City_Name = "Bongaigaon", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "39", City_Name = "Cachar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "40", City_Name = "Charaideo", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "41", City_Name = "Chirang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id=("3") });
            Insert.IntoTable("City").Row(new { Id = "42", City_Name = "Darrang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "43", City_Name = "Dhemaji", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "44", City_Name = "Dhubri", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "45", City_Name = "Dibrugarh", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "46", City_Name = "Dima Hasao", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "47", City_Name = "Goalpara", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "48", City_Name = "Golaghat", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "49", City_Name = "Hailakandi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "50", City_Name = "Hojai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "51", City_Name = "Jorhat", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "52", City_Name = "Kamrup Metropolitan", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "53", City_Name = "Kamrup Rural", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "54", City_Name = "Karbi Anglong", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
            Insert.IntoTable("City").Row(new { Id = "55", City_Name = "Karimganj", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("3") });
          
            //4
            Insert.IntoTable("City").Row(new { Id = "56", City_Name = "Patna", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "57", City_Name = "Gaya", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "58", City_Name = "Muzaffarpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "59", City_Name = "Bhagalpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "60", City_Name = "Darbhanga", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "61", City_Name = "Purnia", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "62", City_Name = "Arrah", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "63", City_Name = "Katihar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "64", City_Name = "Munger", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "65", City_Name = "Bihar Sharif", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "66", City_Name = "Bettiah", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "67", City_Name = "Saharsa", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "68", City_Name = "Sasaram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "69", City_Name = "Hajipur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });
            Insert.IntoTable("City").Row(new { Id = "70", City_Name = "Dehri-on-Sone", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("4") });

                     //5   // Replace biharStateId with the actual state ID for Bihar
                        Insert.IntoTable("City")
                .Row(new { Id = "71", City_Name = "Raipur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "72", City_Name = "Bilaspur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "73", City_Name = "Bhilai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "74", City_Name = "Durg", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "75", City_Name = "Korba", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "76", City_Name = "Raigarh", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "77", City_Name = "Jagdalpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "78", City_Name = "Ambikapur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "79", City_Name = "Rajnandgaon", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "80", City_Name = "Dhamtari", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "81", City_Name = "Janjgir-Champa", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "82", City_Name = "Kawardha", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "83", City_Name = "Mahasamund", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "84", City_Name = "Kanker", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") })
                .Row(new { Id = "85", City_Name = "Bastar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("5") });
                      //6
                        Insert.IntoTable("City")
                .Row(new { Id = "86", City_Name = "Panaji", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") })
                .Row(new { Id = "87", City_Name = "Margao", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") })
                .Row(new { Id = "88", City_Name = "Vasco da Gama", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") })
                .Row(new { Id = "89", City_Name = "Mapusa", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") })
                .Row(new { Id = "90", City_Name = "Ponda", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") })
                .Row(new { Id = "91", City_Name = "Calangute", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") })
                .Row(new { Id = "92", City_Name = "Candolim", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") })
                .Row(new { Id = "93", City_Name = "Mormugao", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("6") });
                      //7
                        Insert.IntoTable("City")
                .Row(new { Id = "94", City_Name = "Ahmedabad", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") })
                .Row(new { Id = "95", City_Name = "Surat", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") })
                .Row(new { Id = "96", City_Name = "Vadodara", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") })
                .Row(new { Id = "97", City_Name = "Rajkot", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") })
                .Row(new { Id = "98", City_Name = "Gandhinagar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") })
                .Row(new { Id = "99", City_Name = "Jamnagar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") })
                .Row(new { Id = "100", City_Name = "Bhavnagar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") })
                .Row(new { Id = "101", City_Name = "Anand", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("7") });

                 //8
                        Insert.IntoTable("City")
                .Row(new { Id = "102", City_Name = "Chandigarh", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") })
                .Row(new { Id = "103", City_Name = "Faridabad", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") })
                .Row(new { Id = "104", City_Name = "Gurugram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") })
                .Row(new { Id = "105", City_Name = "Hisar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") })
                .Row(new { Id = "106", City_Name = "Karnal", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") })
                .Row(new { Id = "107", City_Name = "Panipat", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") })
                .Row(new { Id = "108", City_Name = "Ambala", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") })
                .Row(new { Id = "109", City_Name = "Yamunanagar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("8") });
            //9
            Insert.IntoTable("City").Row(new { Id = "110", City_Name = "Shimla", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });
            Insert.IntoTable("City").Row(new { Id = "111", City_Name = "Manali", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });
            Insert.IntoTable("City").Row(new { Id = "112", City_Name = "Dharamshala", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });
            Insert.IntoTable("City").Row(new { Id = "113", City_Name = "Kullu", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });
            Insert.IntoTable("City").Row(new { Id = "114", City_Name = "Chamba", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });
            Insert.IntoTable("City").Row(new { Id = "115", City_Name = "Mandi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });
            Insert.IntoTable("City").Row(new { Id = "116", City_Name = "Hamirpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });
            Insert.IntoTable("City").Row(new { Id = "117", City_Name = "Palampur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("9") });


            Insert.IntoTable("City")
    .Row(new { Id = "118", City_Name = "Ranchi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "10" })
    .Row(new { Id = "119", City_Name = "Jamshedpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "10" })
    .Row(new { Id = "120", City_Name = "Dhanbad", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "10" })
    .Row(new { Id = "121", City_Name = "Bokaro Steel City", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "10" })
    .Row(new { Id = "122", City_Name = "Hazaribagh", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "10" })
    .Row(new { Id = "123", City_Name = "Deoghar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "10" });



            //10
            Insert.IntoTable("City")
  .Row(new { Id = "124", City_Name = "Bangalore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "11" })
  .Row(new { Id = "125", City_Name = "Mysore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "11" })
  .Row(new { Id = "126", City_Name = "Hubli-Dharwad", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "11" })
  .Row(new { Id = "127", City_Name = "Belgaum", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "11" })
  .Row(new { Id = "128", City_Name = "Mangalore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "11" })
  .Row(new { Id = "129", City_Name = "Gulbarga", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "11" });
            //11
            Insert.IntoTable("City")
      .Row(new { Id = "130", City_Name = "Thiruvananthapuram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "12" })
      .Row(new { Id = "131", City_Name = "Kochi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "12" })
      .Row(new { Id = "132", City_Name = "Kozhikode", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "12" })
      .Row(new { Id = "133", City_Name = "Thrissur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "12" })
      .Row(new { Id = "134", City_Name = "Kollam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "12" })
      .Row(new { Id = "135", City_Name = "Alappuzha", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = "12" });
            //12
            Insert.IntoTable("City")
                .Row(new { Id = "136", City_Name = "Bhopal", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("13") })
                .Row(new { Id = "137", City_Name = "Indore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("13") })
                .Row(new { Id = "138", City_Name = "Jabalpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("13") })
                .Row(new { Id = "139", City_Name = "Gwalior", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("13") })
                .Row(new { Id = "140", City_Name = "Ujjain", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("13") })
                .Row(new { Id = "141", City_Name = "Bhind", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("13") });
            //13
            Insert.IntoTable("City")
    .Row(new { Id = "142", City_Name = "Mumbai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("14") })
    .Row(new { Id = "143", City_Name = "Pune", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("14") })
    .Row(new { Id = "144", City_Name = "Nagpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("14") })
    .Row(new { Id = "145", City_Name = "Nashik", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("14") })
    .Row(new { Id = "146", City_Name = "Aurangabad", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("14") })
    .Row(new { Id = "147", City_Name = "Solapur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("14") });

            Insert.IntoTable("City")
    .Row(new { Id = "148", City_Name = "Imphal", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("15") })
    .Row(new { Id = "149", City_Name = "Thoubal", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("15") })
    .Row(new { Id = "150", City_Name = "Bishnupur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("15") })
    .Row(new { Id = "151", City_Name = "Churachandpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("15") })
    .Row(new { Id = "152", City_Name = "Senapati", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("15") })
    .Row(new { Id = "153", City_Name = "Ukhrul", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("15") });
            Insert.IntoTable("City")
                .Row(new { Id = "154", City_Name = "Shillong", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("16") })
                .Row(new { Id = "155", City_Name = "Tura", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("16") })
                .Row(new { Id = "156", City_Name = "Nongstoin", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("16") })
                .Row(new { Id = "157", City_Name = "Jowai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("16") })
                .Row(new { Id = "158", City_Name = "Baghmara", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("16") });
            //16
            Insert.IntoTable("City")
       .Row(new { Id = "160", City_Name = "Aizawl", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("17") })
       .Row(new { Id = "161", City_Name = "Lunglei", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("17") })
       .Row(new { Id = "162", City_Name = "Champhai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("17") })
       .Row(new { Id = "163", City_Name = "Serchhip", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("17") })
       .Row(new { Id = "164", City_Name = "Kolasib", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("17") });
            Insert.IntoTable("City")
    .Row(new { Id = "165", City_Name = "Kohima", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("18") })
    .Row(new { Id = "166", City_Name = "Dimapur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("18") })
    .Row(new { Id = "167", City_Name = "Mokokchung", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("18") })
    .Row(new { Id = "168", City_Name = "Tuensang", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("18") })
    .Row(new { Id = "169", City_Name = "Wokha", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("18") });
            //18
            Insert.IntoTable("City")
                .Row(new { Id = "170", City_Name = "Bhubaneswar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("19") })
                .Row(new { Id = "171", City_Name = "Cuttack", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("19") })
                .Row(new { Id = "172", City_Name = "Puri", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("19") })
                .Row(new { Id = "173", City_Name = "Rourkela", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("19") })
                .Row(new { Id = "174", City_Name = "Sambalpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("19") })
                .Row(new { Id = "175", City_Name = "Berhampur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("19") });
            Insert.IntoTable("City")
      .Row(new { Id = "176", City_Name = "Chandigarh", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("20") })
      .Row(new { Id = "177", City_Name = "Amritsar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("20") })
      .Row(new { Id = "178", City_Name = "Ludhiana", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("20") })
      .Row(new { Id = "179", City_Name = "Jalandhar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("20") })
      .Row(new { Id = "180", City_Name = "Patiala", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("20") })
      .Row(new { Id = "181", City_Name = "Pathankot", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("20") });
            //20
            Insert.IntoTable("City")
       .Row(new { Id = "182", City_Name = "Jaipur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("21") })
       .Row(new { Id = "183", City_Name = "Jodhpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("21") })
       .Row(new { Id = "184", City_Name = "Udaipur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("21") })
       .Row(new { Id = "185", City_Name = "Kota", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("21") })
       .Row(new { Id = "186", City_Name = "Ajmer", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("21") })
       .Row(new { Id = "187", City_Name = "Bikaner", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("21") });
            //21
            Insert.IntoTable("City")
       .Row(new { Id = "188", City_Name = "Gangtok", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("22") })
       .Row(new { Id = "189", City_Name = "Namchi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("22") })
       .Row(new { Id = "190", City_Name = "Gyalshing", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("22") })
       .Row(new { Id = "191", City_Name = "Rangpo", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("22") })
       .Row(new { Id = "192", City_Name = "Mangan", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("22") });



            Insert.IntoTable("City")
     .Row(new { Id = "193", City_Name = "Alandur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "194", City_Name = "Ambattur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "195", City_Name = "Ambur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "196", City_Name = "Avadi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "197", City_Name = "Chennai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "198", City_Name = "Coimbatore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "199", City_Name = "Cuddalore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "200", City_Name = "Dindigul", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "201", City_Name = "Erode", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "202", City_Name = "Hosur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "203", City_Name = "Kancheepuram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "204", City_Name = "Karaikkudi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "205", City_Name = "Kumbakonam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "206", City_Name = "Kurichi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "207", City_Name = "Madavaram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "208", City_Name = "Madurai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "209", City_Name = "Nagapattinam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "210", City_Name = "Nagercoil", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "211", City_Name = "Neyveli", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "212", City_Name = "Pallavaram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "213", City_Name = "Pudukkottai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "214", City_Name = "Rajapalayam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "215", City_Name = "Salem", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "216", City_Name = "Tambaram", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "217", City_Name = "Thanjavur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "218", City_Name = "Thoothukkudi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "219", City_Name = "Tiruchirappalli", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "220", City_Name = "Tirunelveli", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "221", City_Name = "Tiruppur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "222", City_Name = "Tiruvannamalai", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "223", City_Name = "Tiruvottiyur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") })
     .Row(new { Id = "224", City_Name = "Vellore", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("23") });

            Insert.IntoTable("City")
        .Row(new { Id = "225", City_Name = "Hyderabad", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("24") })
        .Row(new { Id = "226", City_Name = "Warangal", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("24") })
        .Row(new { Id = "227", City_Name = "Nizamabad", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("24") })
        .Row(new { Id = "228", City_Name = "Karimnagar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("24") })
        .Row(new { Id = "229", City_Name = "Khammam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("24") })
        .Row(new { Id = "230", City_Name = "Ramagundam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("24") });


            // Block 26
            // Block 25
            Insert.IntoTable("City")
                .Row(new { Id = "231", City_Name = "Agartala", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("25") })
                .Row(new { Id = "232", City_Name = "Udaipur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("25") })
                .Row(new { Id = "233", City_Name = "Dharmanagar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("25") })
                .Row(new { Id = "234", City_Name = "Kailashahar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("25") })
                .Row(new { Id = "235", City_Name = "Ambassa", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("25") });

            // Block 26
            Insert.IntoTable("City")
                .Row(new { Id = "236", City_Name = "Dehradun", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("26") })
                .Row(new { Id = "237", City_Name = "Haridwar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("26") })
                .Row(new { Id = "238", City_Name = "Nainital", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("26") })
                .Row(new { Id = "239", City_Name = "Roorkee", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("26") })
                .Row(new { Id = "240", City_Name = "Haldwani", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("26") });

            // Block 27
            Insert.IntoTable("City")
                .Row(new { Id = "241", City_Name = "Lucknow", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("27") })
                .Row(new { Id = "242", City_Name = "Kanpur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("27") })
                .Row(new { Id = "243", City_Name = "Agra", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("27") })
                .Row(new { Id = "244", City_Name = "Varanasi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("27") })
                .Row(new { Id = "245", City_Name = "Prayagraj", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("27") });

           // Continue with the rest of the blocks...

            // Block 29
            Insert.IntoTable("City")
                .Row(new { Id = "246", City_Name = "Kolkata", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("28") })
                .Row(new { Id = "247", City_Name = "Asansol", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("28") })
                .Row(new { Id = "248", City_Name = "Siliguri", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("28") })
                .Row(new { Id = "249", City_Name = "Durgapur", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("28") })
                .Row(new { Id = "250", City_Name = "Howrah", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("28") });
            //andhaman************************************
            //30

            Insert.IntoTable("City")
              .Row(new { Id = "251", City_Name = "Nicobar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("29") })
              .Row(new { Id = "252", City_Name = "North and Middle Andaman", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("29") })
              .Row(new { Id = "253", City_Name = "South Andaman", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("29") });






            Insert.IntoTable("City")
                .Row(new { Id = "254", City_Name = "Chandigarh", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("30") });

            //31
            Insert.IntoTable("City")
                .Row(new { Id = "255", City_Name = "Daman", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("31") })
                .Row(new { Id = "256", City_Name = "Silvassa", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("31") });

            //32
            Insert.IntoTable("City")
                .Row(new { Id = "257", City_Name = "Central Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "258", City_Name = "East Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "259", City_Name = "New Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "260", City_Name = "North Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "261", City_Name = "North East Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "262", City_Name = "North West Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "263", City_Name = "South Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "264", City_Name = "South East Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "265", City_Name = "South West Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") })
                .Row(new { Id = "266", City_Name = "West Delhi", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("32") });

            //33
            Insert.IntoTable("City")
                .Row(new { Id = "267", City_Name = "Srinagar", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("33") })
                .Row(new { Id = "268", City_Name = "Jammu", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("33") });

            //34
            Insert.IntoTable("City")
                .Row(new { Id = "269", City_Name = "Leh", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("34") })
                .Row(new { Id = "270", City_Name = "Kargil", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("34") });

            //35
            Insert.IntoTable("City")
                .Row(new { Id = "271", City_Name = "Kavaratti", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("35") });

            //36
            Insert.IntoTable("City")
                .Row(new { Id = "272", City_Name = "Puducherry", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("36") })
                .Row(new { Id = "273", City_Name = "Karaikal", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("36") })
                .Row(new { Id = "274", City_Name = "Mahe", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("36") })
                .Row(new { Id = "275", City_Name = "Yanam", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, State_Id = ("36") });

        }



    }

    }
