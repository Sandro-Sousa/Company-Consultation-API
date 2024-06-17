import Vue3Toastify, {
  type Content,
  type ToastContainerOptions,
  toast,
} from "vue3-toastify";
import "vue3-toastify/dist/index.css";

export default defineNuxtPlugin(({vueApp}) => {
  vueApp.use(Vue3Toastify, {
      autoclose: 2000,
  } as ToastContainerOptions);

  toast.error = (message: Content) =>
      toast(message, {
        type: "error",
        position: "top-right",
        icon: "❌",
        theme: "dark",
      });

  toast.success = (message: Content) =>
      toast(message, {
        type: "success",
        position: "top-right",
        icon: "✔️",
        theme: "dark",
      });

  return { 
      provide: { 
        toast,
      },
  };
});
