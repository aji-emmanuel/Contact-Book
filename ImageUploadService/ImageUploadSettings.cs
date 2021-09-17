using System;
using System.Collections.Generic;
using System.Text;

namespace ImageUploadService
{
    /// <summary>
    /// A model of the properties required for an image upload
    /// </summary>
    public class ImageUploadSettings
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
