using System;
using System.Collections.Generic;
using System.Text;

namespace TestEFInserts.Entities
{
    public class SonWithAutoParent
    {
        private int _parentId;

        public int SonWithAutoParentId { get; set; }
        public string Name { get; set; }
        public int ParentId
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.ParentId;
                }
                else return -1;
            }

            set { _parentId = value; }
        }
        public Parent Parent { get; set; }
    }
}
