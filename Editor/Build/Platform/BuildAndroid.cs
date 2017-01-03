using UnityEditor;

namespace Unitybuild
{
    [System.Serializable]
    class BuildAndroid : BuildPlatform
    {
        #region Constants (SET VALUES)

        private const string _name = "Android";
        private const string _binaryNameFormat = "{0}.apk";
        private const string _dataDirNameFormat = "";

        #endregion

        public BuildAndroid()
        {
            enabled = false;
            platformName = _name;
            architectures = new BuildArchitecture[] 
            {
                new BuildArchitecture(BuildTarget.Android, "Android", true)
            };
        }

        #region Methods & Properties (DO NOT EDIT)

        public override string binaryNameFormat
        {
            get { return _binaryNameFormat; }
        }

        public override string dataDirNameFormat
        {
            get { return _dataDirNameFormat; }
        }

        #endregion
    }
}
