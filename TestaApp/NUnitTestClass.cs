﻿using NUnit.Framework;
using System;
using QRCode;
using System.Collections.Generic;

namespace TestaApp
{
    [TestFixture]
    public class NUnitTestClass
    {
        private byte[] bytes_QRCode;
        private static Dictionary<string, string> source;

        [SetUp]
        public void Data_List_Bite()
        {
            byte[] bytes = { 83, 84, 48, 48,  48, 49, 50, 124, 78, 97, 109,
                101, 61, 208, 158, 208, 158, 208, 158, 34, 208, 163, 208, 150,
                208, 173, 208, 154, 34, 208, 148, 208, 190, 208, 188, 208, 190,
                209, 131, 208, 191, 209, 128, 208, 176, 208, 178, 34, 124, 80,
                101, 114, 115, 111, 110, 97, 108, 65, 99, 99, 61, 52, 48, 55, 48,
                50, 56, 49, 48, 56, 55, 50, 48, 48, 48, 48, 49, 50, 55, 48, 53,
                124, 66, 97, 110, 107, 78, 97, 109, 101, 61, 208, 159, 208, 144,
                208, 158, 32, 34, 208, 161, 208, 177, 208, 181, 209, 128, 208,
                177, 208, 176, 208, 189, 208, 186, 32, 208, 160, 208, 190, 209,
                129, 209, 129, 208, 184, 208, 184, 34, 32, 124, 66, 73, 67, 61,
                48, 52, 55, 53, 48, 49, 54, 48, 50, 124, 67, 111, 114, 114, 101,
                115, 112, 65, 99, 99, 61, 51, 48, 49, 48, 49, 56, 49, 48, 55, 48,
                48, 48, 48, 48, 48, 48, 48, 54, 48, 50, 124, 80, 97, 121, 101,
                101, 73, 78, 78, 61, 55, 52, 52, 53, 48, 51, 52, 50, 53, 52,
                124, 76, 97, 115, 116, 78, 97, 109, 101, 61, 208, 167, 208, 184,
                208, 179, 208, 190, 209, 128, 208, 184, 208, 189, 124, 70, 105,
                114, 115, 116, 78, 97, 109, 101, 61, 208, 164, 208, 181, 208,
                180, 208, 190, 209, 128, 124, 77, 105, 100, 100, 108, 101, 78,
                97, 109, 101, 61, 208, 159, 208, 181, 209, 130, 209, 128, 208,
                190, 208, 178, 208, 184, 209, 135, 209, 129, 124, 80, 101, 114,
                115, 65, 99, 99, 61, 49, 48, 48, 48, 57, 52, 54, 48, 57, 49, 124,
                112, 97, 121, 109, 80, 101, 114, 105, 111, 100, 61, 48, 54, 46,
                50, 48, 49, 55, 124, 80, 97, 121, 101, 114, 65, 100, 100, 114,
                101, 115, 115, 61, 32, 209, 131, 208, 187, 46, 208, 150, 209,
                131, 208, 186, 208, 190, 208, 178, 208, 176, 44, 32, 208, 180,
                46, 50, 53, 47, 208, 154, 208, 176, 208, 191, 46, 209, 128, 44,
                32, 208, 186, 46, 49, 52, 124, 83, 117, 109, 61, 56, 52, 51, 53,
                48, 53, 124, 80, 117, 114, 112, 111, 115, 101, 61, 208, 154,
                208, 190, 208, 188, 208, 188, 209, 131, 208, 189, 208, 176,
                208, 187, 209, 140, 208, 189, 209, 139, 208, 181, 32, 209, 131,
                209, 129, 208, 187, 209, 131, 208, 179, 208, 184, 44, 32, 208,
                150, 208, 154, 208, 165
            };

            bytes_QRCode = bytes;
        }

        [Test]
        public void Camera()
        {
            try
            {
                var dictionary = Function.Parsing_Text(bytes_QRCode);

                if (dictionary.Count == 0)
                    Assert.AreEqual(0, dictionary.Count);

                Assert.AreNotEqual(0, dictionary.Count);
                source = dictionary;
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Get_HTML_Props_Table()
        {
            try
            {
                var result = Function.Get_HTML_Props_Table(source);

                if(string.IsNullOrEmpty(result))
                    Assert.IsNullOrEmpty(result);

                Assert.IsNotNullOrEmpty(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
