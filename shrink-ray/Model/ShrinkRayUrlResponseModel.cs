using System;
using System.Collections.Generic;

namespace Model
{
    public class ShrinkRayUrlResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public ShrinkRayUrlModel Model { get; set; }
    }
}