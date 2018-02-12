using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;



using System.Windows.Forms;
using Amazon.S3.Transfer;

namespace s3Samples

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        //Download a file from s3 bucket.
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        
        static IAmazonS3 GetAmazonS3Client()
        {
            

            AmazonS3Config config = new AmazonS3Config();
            config.RegionEndpoint = RegionEndpoint.APSoutheast2;
            config.Timeout = TimeSpan.FromSeconds(3);           // Default value is 100 seconds
            config.ReadWriteTimeout = TimeSpan.FromSeconds(3);   // Default value is 300 seconds
            config.MaxErrorRetry = 2;

            IAmazonS3 s3Client = AWSClientFactory.CreateAmazonS3Client(
                
                    );

            return s3Client;


        }


        //Upload a file to s3 bucket
        private void btnUpload_Click(object sender, EventArgs e)
        {
            IAmazonS3 client;

            AmazonS3Config s3 = new AmazonS3Config();

            s3.RegionEndpoint = RegionEndpoint.APSoutheast2;

            client = new AmazonS3Client("<Accesskey>", "<SecureAccesskey>", s3); ;

            TransferUtility utility = new TransferUtility(client);

            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();


            request.BucketName = "test-s3-bucket/PDFFolder/";
            request.Key = "Test3.pdf"; //file name up in S3"
            request.FilePath = @"C:\Temp\Test3.pdf"; //local file name
            utility.Upload(request); //commensing the transfer
        }


        //Download a file from S3 bucket.

        private void btnDownload_Click(object sender, EventArgs e)
        {
            AmazonS3Config config = new AmazonS3Config();
            config.RegionEndpoint = RegionEndpoint.APSoutheast2;
            config.Timeout = TimeSpan.FromSeconds(3);           // Default value is 100 seconds
            config.ReadWriteTimeout = TimeSpan.FromSeconds(3);  // Default value is 300 seconds
            config.MaxErrorRetry = 2;

            //Set the Access key and SecreateAccesskeys specific to your envi.

            AmazonS3Client c1 = new AmazonS3Client("<AccessKey>", "<SecreateAccessKey>", config);

            using (c1)
            {

                try
                {
                    GetObjectResponse r = c1.GetObject
                    (
                         new GetObjectRequest()
                         {
                             BucketName = "test-s3-bucket", // Replace this with your S3 bucket names
                             Key = "PDFFolder/Test.pdf"     // Replace this with your S3 buceket folder name and 
                                                            //file name that you want to download
                         }


                    );
                    // This is the location on local machine where you want to maintain 
                    // file.
                    r.WriteResponseStreamToFile(@"C:\Temp\Test.pdf", false);

                }
                catch { }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
