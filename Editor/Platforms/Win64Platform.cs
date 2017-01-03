using UnityEditor;

namespace UnityBuild
{

    public class Win64Platform : BuildPlatform
    {
        #region Constants (SET VALUES)

        private const BuildTarget _target = BuildTarget.StandaloneWindows64;
        private const string _name = "Win64";
        private const string _binaryNameFormat = "{0}.exe";
        private const string _dataDirNameFormat = "{0}_Data";

        #endregion

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

        #endregion
    }

}