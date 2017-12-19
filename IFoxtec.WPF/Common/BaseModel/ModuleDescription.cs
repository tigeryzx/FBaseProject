using DevExpress.Images;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IFoxtec.WPF.Common.BaseModel
{
    /// <summary>
    /// 模块描述信息
    /// </summary>
    public class ModuleDescription
    {
        public ModuleDescription()
        {

        }

        public ModuleDescription(string moduleGroup,List<MenuDescription> menus)
        {
            this.ModuleGroup = moduleGroup;
            this.Menus = menus;

            this.Icon = DXImageHelper.GetImageSource(DXImages.Open, ImageSize.Size32x32);
        }

        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<MenuDescription> Menus { get; set; }

        /// <summary>
        /// 模块分组
        /// </summary>
        public string ModuleGroup { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource Icon { get; set; }
    }
}
