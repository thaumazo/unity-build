using UnityEditor;

namespace UnityBuild
{
    public class Linux32Platform : BuildPlatform
    {
        #region Constants

        private const BuildTarget _target = BuildTarget.StandaloneLinux;
        private const string _name = "Linux32";
        private const string _binaryNameFormat = "{0}.x86";
        private const string _dataDirNameFormat = "{0}_Data";

        #endregion Constants

        #region Methods & Properties (DO NOT EDIT)

        public override BuildTarget target
        {
            get { return _target; }
        }

        public override string name
        {
            get { return _name; }
        }

        public override string binaryNameFormat
        {
            get { return _binaryNameFormat; }
        }

        public override string dataDirNameFormat
        {
            get { return _dataDirNameFormat; }
        }

        [MenuItem("Build/Platforms/" + _name)]
        private static void Toggle()
        {
            Toggle(_name);
        }

        [MenuItem("Build/Platforms/" + _name, true)]
        private static bool ToggleValidate()
        {
            return ToggleValidate(_name);
        }

        #endregion Methods & Properties (DO NOT EDIT)
    }
}