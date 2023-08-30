using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Storage;
using Stream = System.IO.Stream;
using Encoding = System.Text.Encoding;
using System.Collections.Specialized;
using System.Net;

namespace REPORT
{
    public class firebaseConnection
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://reportit-6bf64-default-rtdb.asia-southeast1.firebasedatabase.app/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("reportit-6bf64.appspot.com");
        public async Task<bool> Save(userInfo user)
        {
            try
            {
                await firebaseClient.Child(nameof(userInfo))
                                    .Child(Signup.userID)
                                    .PutAsync(JsonConvert.SerializeObject(user));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        /*
         * remove comment only when adding dummy accounts
        public async Task<bool> SaveDummyAccounts(userInfo user)
        {
            try
            {
                await firebaseClient.Child("DummyAccounts2")
                                    .Child(Signup.accountID)
                                    .PutAsync(JsonConvert.SerializeObject(user));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        */


        //adds users to firebase database
        public async Task<bool> AddUserInfo(userInfo user)
        {
            try
            {
                await firebaseClient.Child(nameof(userInfo))
                                    .Child(AddUser.userID)
                                    .PutAsync(JsonConvert.SerializeObject(user));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        //updates data from specific node 
        //much better if kaya nya mag update ng multiple nodes since pede iupdate yung last name
        public async Task<bool> Update(userInfo user)
        {
            try
            {
                await firebaseClient.Child(nameof(userInfo))
                                    .Child(ChooseUser.userID)
                                    .PatchAsync(JsonConvert.SerializeObject(user));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        
        public async Task<bool> SavelostInfo(lostandfoundInfo lostitemsInfo)
        {
            try
            {
                await firebaseClient.Child("LostandFoundReports")
                                    .Child(LostAndFound.getID)
                                    .PutAsync(JsonConvert.SerializeObject(lostitemsInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveSubdivisionHazards(subdivisionHazardsInfo subdivisionhazardsInfo)
        {
            try
            {
                await firebaseClient.Child("SubdivisionHazardsReports")
                                    .Child(SubdivisionHazards.getID)
                                    .PutAsync(JsonConvert.SerializeObject(subdivisionhazardsInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveNeighborhoodConcernsInfo(neighborhoodconcernsInfo neighborhoodConcernsInfo)
        {
            try
            {
                await firebaseClient.Child("NeighborhoodConcernsReports")
                                    .Child(NeighborhoodConcerns.getID)
                                    .PutAsync(JsonConvert.SerializeObject(neighborhoodConcernsInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<bool> SavePowerOutageInfo(poweroutageInfo powerOutageInfo)
        {
            //var data = await firebaseClient.Child(nameof(poweroutageInfo)).PostAsync(JsonConvert.SerializeObject(powerOutageInfo));
            try {
                await firebaseClient.Child("PowerOutageReports")
                                    .Child(PowerOutage.getID)
                                    .PutAsync(JsonConvert.SerializeObject(powerOutageInfo));
                return true;
            } catch (Exception exception)
            {
                return false;
            }


            /*if (!string.IsNullOrEmpty(data.Key))a
            {
                return true;
            }
            return false;
            */
        }

        public async Task<bool> SavewaterInterruptionInfo(waterinterruptionInfo waterInterruptionInfo)
        {

            try
            {
                await firebaseClient.Child("WaterInterruptionReports")
                                    .Child(WaterInterruption.getID)
                                    .PutAsync(JsonConvert.SerializeObject(waterInterruptionInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        /*
        public async Task<bool> SaveOthers(OthersInfo OthersInfo)
        {
            try
            {
                await firebaseClient.Child(nameof(waterinterruptionInfo))
                                    .PutAsync(JsonConvert.SerializeObject(OthersInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }


        }
        */
        public async Task<bool> SaveConstructionWorkerIssueInfo(constructionworkerissuesInfo constructionworkerissueInfo)
        {
            try
            {
                await firebaseClient.Child("ConstructionWorkerReports")
                                    .Child(ConstructionWorkersIssues.getID)
                                    .PutAsync(JsonConvert.SerializeObject(constructionworkerissueInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<bool> saveReport(reportInformation reportinfo, string child, string reportID)
        {
            try{
                await firebaseClient.Child(child)
                                    .Child(reportID)
                                    .PutAsync(JsonConvert.SerializeObject(reportinfo));
                return true;

            }
            //tamad pako gawan ng exception. 
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveIllegalVendorInfo(illegalVendorsInfo illegalvendorsInfo)
        {
            try
            {
                await firebaseClient.Child("IllegalVendorReports")
                                    .Child(IllegalVendors.getID)
                                    .PutAsync(JsonConvert.SerializeObject(illegalvendorsInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }


        public async Task<bool> SaveUnauthorizedSolicitation(unauthorizedSolicitationInfo unauthorizedsolicitationInfo)
        {
            try
            {
                await firebaseClient.Child("UnauthorizedSolicitationReports")
                                    .Child(UnauthorizedSolicitation.getID)
                                    .PutAsync(JsonConvert.SerializeObject(unauthorizedsolicitationInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
      
        public async Task<bool> SavePermanentParkingInfo(permanentParkingInfo permanentparkingInfo)
        {
            try
            {
                await firebaseClient.Child("PermanentParkingReports")
                                    .Child(PermanentParking.getID)
                                    .PutAsync(JsonConvert.SerializeObject(permanentparkingInfo));
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        //generate incrementing ID for reports
        public async Task<string> generateReportID(string reportCode)
        {
            var getCount = await getAllReports();

            int reportCounter =  getCount.Count();
            reportCounter += 1;
            string reportID = reportCode + "-" + reportCounter.ToString("D6");


            return reportID;

        }
        //if ever magamit man, for data encryption sa database at admin
        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        //retrieves all users from userInfo parent 
        public async Task<List<userInfo>> GetAllUser()
        {

            var userlist = (await firebaseClient.Child("userInfo").OnceAsync<userInfo>()).Select(item => new userInfo
            {
                PhoneNumber = item.Object.PhoneNumber,
                password = item.Object.password,
                userID = item.Object.userID,
                userToken = item.Object.userToken,
                Firstname = item.Object.Firstname,
                Lastname = item.Object.Lastname,
                block = item.Object.block,
                lot = item.Object.lot,
                street = item.Object.street,
                accountID = item.Object.accountID
            }).ToList();
            return userlist;
        }


        public async Task<userInfo> GetUser(string PhoneNumber)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebaseClient.Child("userInfo").OnceAsync<userInfo>();
                return allUsers.Where(a => a.PhoneNumber == PhoneNumber).FirstOrDefault();

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<userInfo> GetProfileSelecteduser(string userID)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebaseClient.Child("userInfo").OnceAsync<userInfo>();
                return allUsers.Where(a => a.userID == userID).FirstOrDefault();

            }
            catch (Exception e)
            {
                return null;
            }
        }

        //retrieves specific account
        public async Task<List<userInfo>> GetAllUserFromSpecificAccount(string accountID)
        {
            var allUsers = await GetAllUser();
            await firebaseClient.Child("userInfo").OnceAsync<userInfo>();
            return allUsers.Where(a => a.accountID == accountID).ToList();
        }

       
       //checks if block and lot is taken
        public async Task<userInfo> checkBlock(string block, string lot)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebaseClient.Child("userInfo").OnceAsync<userInfo>();
                return allUsers.Where(a => a.block == block).Where(b => b.lot == lot).FirstOrDefault();

            }
            catch (Exception e)
            {
                return null;
            }
        }

        //aaa

        public string userSignInToken()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 12);

        }
        //generate account id for accounts
        public string generateAccountID(string firstName, string lastName, string block, string lot, string phoneNumber)
        {

            string sampleID = firstName + " " + lastName;
            string getFirstchars = "";
            string getLastDigit = phoneNumber.Substring(phoneNumber.Length - 2, 2);
            //   return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            foreach (var part in sampleID.Split(' '))
            {
                getFirstchars += part.Substring(0, 1);
            }


            string generateID = string.Concat(getFirstchars, block, lot, getLastDigit);

            return generateID;

        }
        public string generateUserID()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }


        //generates OTP number
        public int generateOTPNumber()
        {
            Random otpNumber = new Random();

            return otpNumber.Next(100000, 999999);


        }

        //uploads image to firebase storage
        public async Task<string> uploadPictureToStorage(Stream img, string fileName, string reportID)
        {
            var image = await firebaseStorage.Child("Test").Child(reportID).Child(fileName).PutAsync(img);
            return image;
        }

        //send OTP values to semaphore link
        //para iwas  ban sa semaphore, mas oks na nakaprivate yung repo or leave blank when pushing new code to repo.
        public void OTPtextInfo(string codeNumber, string number)
        {
            string message = "NEVER SHARE YOUR OTP especially on social media and SMS or email links.";

            using (WebClient client = new WebClient())
            {
                byte[] response =
                client.UploadValues("https://api.semaphore.co/api/v4/otp", new NameValueCollection()
                {
                    { "apikey", "api key" },
                    { "number", number },
                    { "message", message },
                    { "sendername", "TACDesk" },
                    { "code", codeNumber },
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                Console.WriteLine(result);
            }

        }
       
        //searches report by report id, report type, or user ID
        public async Task<List<reports>> getSearchedReportType(string searchVal)
        {

            var getReports = await concatReports();
            await firebaseClient.Child(nameof(reports)).OnceAsync<reports>();
            return getReports.Where(c => c.reportID.Contains(searchVal.ToUpper()) || c.reportType.Contains(searchVal) || c.userID.Contains(searchVal)).ToList();

        }


        public async Task<List<reports>> getAllCWIReports()
        {
            return (await firebaseClient.Child("ConstructionWorkerReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName,
                Others = item.Object.Others

            }).ToList();
        }
        public async Task<List<reports>> getApprovedCWIReports()
        {
            var getLAFReports = await getAllCWIReports();
            await firebaseClient.Child("LostandFoundReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }

        public async Task<List<reports>> getAllLAFReports()
        {
            return (await firebaseClient.Child("LostandFoundReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName

            }).ToList();
        }
        public async Task<List<reports>> getApprovedLAFReports()
        {
            var getLAFReports = await getAllLAFReports();
            await firebaseClient.Child("LostandFoundReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }

        public async Task<List<reports>> getAllIVReports()
        {
            return (await firebaseClient.Child("IllegalVendorReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName,
                Others = item.Object.Others

            }).ToList();
        }
         public async Task<List<reports>> getApprovedIVReports()
         {
            var getLAFReports = await getAllIVReports();
            await firebaseClient.Child("IllegalVendorReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


         }

        public async Task<List<reports>> getAllNCReports()
        {
            return (await firebaseClient.Child("NeighborhoodConcernsReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName

            }).ToList();
        }
        public async Task<List<reports>> getApprovedNCReports()
        {
            var getLAFReports = await getAllNCReports();
            await firebaseClient.Child("NeighborhoodConcernsReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }
        public async Task<List<reports>> getAllPOReports()
        {
            return (await firebaseClient.Child("PowerOutageReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName

            }).ToList();
        }
        public async Task<List<reports>> getApprovedPOReports()
        {
            var getLAFReports = await getAllPOReports();
            await firebaseClient.Child("PowerOutageReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }

        public async Task<List<reports>> getAllPPReports()
        {
            return (await firebaseClient.Child("PermanentParkingReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName

            }).ToList();
        }
        public async Task<List<reports>> getApprovedPPReports()
        {
            var getLAFReports = await getAllPPReports();
            await firebaseClient.Child("PermanentParkingReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }

        public async Task<List<reports>> getAllSHReports()
        {
            return (await firebaseClient.Child("SubdivisionHazardsReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName,
                Others = item.Object.Others

            }).ToList();
        }
        public async Task<List<reports>> getApprovedSHReports()
        {
            var getLAFReports = await getAllSHReports();
            await firebaseClient.Child("SubdivisionHazardsReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }
        public async Task<List<reports>> getAllUSReports()
        {
            return (await firebaseClient.Child("UnauthorizedSolicitationReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName

            }).ToList();
        }
        public async Task<List<reports>> getApprovedUSReports()
        {
            var getLAFReports = await getAllUSReports();
            await firebaseClient.Child("UnauthorizedSolicitationReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }
        public async Task<List<reports>> getAllWIReports()
        {
            return (await firebaseClient.Child("WaterInterruptionReports").OnceAsync<reports>()).Select(item => new reports()
            {
                AdditionalInfo = item.Object.AdditionalInfo,
                reportID = item.Object.reportID,
                userID = item.Object.userID,
                Image = item.Object.Image,
                IsApproved = item.Object.IsApproved,
                timeAndDateReportPosted = item.Object.timeAndDateReportPosted,
                ReportStatus = item.Object.ReportStatus,
                reportType = item.Object.reportType,
                issueName = item.Object.issueName

            }).ToList();
        }
        public async Task<List<reports>> getApprovedWIReports()
        {
            var getLAFReports = await getAllWIReports();
            await firebaseClient.Child("WaterInterruptionReports").OnceAsync<reports>();
            return getLAFReports.Where(a => a.IsApproved.Contains("Approved")).ToList();


        }
        public async Task<List<reports>> getBECReports()
        {
            return (await firebaseClient.Child("BECollapseReports").OnceAsync<reports>()).Select(item => new reports()
            {
                Location= item.Object.Location,
                reportID = item.Object.reportID
                

            }).ToList();
        }
        public async Task<List<reports>> getCAReports()
        {
            return (await firebaseClient.Child("CarAccidentReports").OnceAsync<reports>()).Select(item => new reports()
            {
                Location = item.Object.Location,
                reportID = item.Object.reportID


            }).ToList();
        }
        public async Task<List<reports>> getFIReports()
        {
            return (await firebaseClient.Child("FireIncidentReports").OnceAsync<reports>()).Select(item => new reports()
            {
                Location = item.Object.Location,
                reportID = item.Object.reportID


            }).ToList();
        }
        public async Task<List<reports>> getFFReports()
        {
            return (await firebaseClient.Child("FlashFloodReports").OnceAsync<reports>()).Select(item => new reports()
            {
                Location = item.Object.Location,
                reportID = item.Object.reportID


            }).ToList();
        }
        public async Task<List<reports>> getMEReports()
        {
            return (await firebaseClient.Child("MedicalEmergencyReports").OnceAsync<reports>()).Select(item => new reports()
            {
                Location = item.Object.Location,
                reportID = item.Object.reportID


            }).ToList();
        }
        public async Task<List<reports>> getRIReports()
        {
            return (await firebaseClient.Child("RIReports").OnceAsync<reports>()).Select(item => new reports()
            {
                Location = item.Object.Location,
                reportID = item.Object.reportID


            }).ToList();
        }
        public async Task<List<reports>> getSFReports()
        {
            return (await firebaseClient.Child("StreetFightReports").OnceAsync<reports>()).Select(item => new reports()
            {
                Location = item.Object.Location,
                reportID = item.Object.reportID


            }).ToList();
        }

        //concats all approved posts for home page 
        public async Task<List<reports>> concatReports()
        {
            var getLAFApproved = await getApprovedLAFReports();
            var getCWIApproved = await getApprovedCWIReports();
            var getNCApproved = await getApprovedNCReports();
            var getPPApproved = await getApprovedPPReports();
            var getIVApproved = await getApprovedIVReports();
            var getPOApproved = await getApprovedPOReports();
            var getSHApproved = await getApprovedSHReports();
            var getUSApproved = await getApprovedUSReports();
            var getWIpproved = await getApprovedWIReports();

            var callReports = await getReportsFromCall();



            var concatList = getLAFApproved.Concat(getCWIApproved)
                            .Concat(getNCApproved)
                            .Concat(getPPApproved)
                            .Concat(getIVApproved)
                            .Concat(getPOApproved)
                            .Concat(getSHApproved)
                            .Concat(getUSApproved)
                            .Concat(getWIpproved)
                            .Concat(callReports);
                            

           return concatList.OrderByDescending(a => a.timeAndDateReportPosted).ToList();
        }

        
        public async Task<List<reports>> getUserHistory(string userID)
        {
            var getReports = await getAllReports();
            return getReports.Where(a => a.userID == userID).ToList();
        }

        public async Task<List<reports>> getAllReports()
        {
           
            var getLAFApproved = await getAllLAFReports();
            var getCWIApproved = await getAllCWIReports();
            var getNCApproved = await getAllNCReports();
            var getPPApproved = await getAllPPReports();
            var getIVApproved = await getAllIVReports();
            var getPOApproved = await getAllPOReports();
            var getSHApproved = await getAllSHReports();
            var getUSApproved = await getAllUSReports();
            var getWIpproved = await getAllWIReports();

            var concatList = getLAFApproved.Concat(getCWIApproved)
                             .Concat(getNCApproved)
                             .Concat(getPPApproved)
                             .Concat(getIVApproved)
                             .Concat(getPOApproved)
                             .Concat(getSHApproved)
                             .Concat(getUSApproved)
                             .Concat(getWIpproved);


            return concatList.OrderByDescending(a => a.timeAndDateReportPosted).ToList();
        }

        public async Task<List<reports>> getReportsFromCall()
        {
            var BECReports = await getBECReports();
            var CAReports = await getCAReports();
            var FIReports = await getFIReports();
            var FFReports = await getFFReports();
            var MEReports = await getMEReports();
            var RIReports = await getRIReports();
            var SFReports = await getSFReports();


            var concatReportsFromCallList = BECReports.Concat(CAReports)
                                            .Concat(FIReports)
                                            .Concat(FFReports)
                                            .Concat(MEReports)
                                            .Concat(RIReports)
                                            .Concat(SFReports);

            
            return concatReportsFromCallList.ToList();
        }


    }
}
