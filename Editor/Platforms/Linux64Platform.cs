using UnityEditor;

namespace UnityBuild
{
    public class Linux64Platform : BuildPlatform
    {
        #region Constants (SET VALUES)

        private const BuildTarget _target = BuildTarget.StandaloneLinux64;
        private const string _name = "Linux64";
        private const string _binaryNameFormat = "{0}.x86_64";
        private const string _dataDirNameFormat = "{0}_Data";

        #endregion Constants (SET VALUES)

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