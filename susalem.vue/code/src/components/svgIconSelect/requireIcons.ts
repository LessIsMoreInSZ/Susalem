let icons = [] as any
// const modules = import.meta.glob('./../../assets/icons/svg/*.svg')
// for (const path in modules) {
//   const p = path.split('assets/icons/svg/')[1].split('.svg')[0]
//   icons.push(p)
// }

// iconfont
import iconList from "../../assets/iconfont/iconfont.json"

iconList.glyphs.forEach((element: any) => {
  icons.push(element.name)
})
export default icons
