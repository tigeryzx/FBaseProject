using DevExpress.Images;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IFoxtec.Common.WPF.BaseModel
{
    /// <summary>
    /// 菜单描述
    /// </summary>
    public class MenuDescription
    {
        public MenuDescription()
        {

        }

        public MenuDescription(string menuTitle)
        {
            this.MenuTitle = menuTitle;

            this.Icon = DXImageHelper.GetImageSource(DXImages.Apply, ImageSize.Size16x16);
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuTitle { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public string DocumnetType { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource Icon { get; set; }
    }
}
