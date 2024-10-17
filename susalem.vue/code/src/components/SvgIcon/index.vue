<script lang="ts">
import { h, resolveComponent, defineComponent, computed, toRefs } from "vue"

export default defineComponent({
  props: {
    // svg 图标组件名字
    name: {
      type: String,
      required: true,
      default: ""
    },
    className: {
      type: String,
      default: ""
    },
    // svg 颜色
    color: {
      type: String,
      default: ""
    }
  },
  setup(props) {
    const { name, className, color } = toRefs(props)

    if (name.value?.startsWith("ele")) {
      return () =>
        h(
          "i",
          {
            class: "el-icon"
          },
          [h(resolveComponent(name.value.replace("ele-", "")))]
        )
    } else if (name.value != undefined && name.value != "") {
      return () =>
        h(
          "svg",
          {
            name: name.value,
            "aria-hidden": true,
            style: `color: ${color.value}`,
            class: `svg-icon ${className.value}`,
            "shape-rendering": "geometricPrecision"
          },
          h("use", {
            "xlink:href": `#icon-${name.value}`,
            fill: `${color.value}`
          })
        )
    } else {
      return () => h("i")
    }
  }
})
</script>

<style lang="scss" scope>
.svg-icon {
  width: 1em;
  height: 1em;
  vertical-align: -0.15em;
  fill: currentColor;
  overflow: hidden;
}
</style>
