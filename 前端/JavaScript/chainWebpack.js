// https://umijs.org/config/
import { defineConfig } from 'umi';
import proxy from './proxy';
import pageRoutes from './router.config';
import defaultSettings from './defaultSettings';

const { REACT_APP_ENV } = process.env;
export default defineConfig({
  hash: true,
  antd: {
    // dark: true
  },
  // mfsu: {},
  // webpack5: {},
  dynamicImport: {
    loading: '@/components/PageLoading',
  },
  dva: {
    hmr: true,
  },
  locale: {
    default: 'zh-CN',
    //default: 'en-US',
    // default true, when it is true, will use `navigator.language` overwrite default
    antd: true,
    baseNavigator: true,
  },

  targets: {
    ie: 11,
  },
  // umi routes: https://umijs.org/docs/routing
  routes: pageRoutes,
  // Theme for antd: https://ant.design/docs/react/customize-theme-cn
  theme: {
    // '@ptl-header-background': '#252832',
    // '@layout-sider-background': '#313541',
    // '@ptl-sidermenu-submenu-bg': '#252832',
    // '@ptl-sidermenu-selected-bg': 'rgba(214,81,81,0.2)',
    // '@ptl-subnav-active-bg': 'rgba(214,81,81,0.2)',
    // '@ptl-subnav-selected-color': '#ffffff',
    // '@ptl-subnav-active-color': '#ffffff',
    // '@ptl-header-logo-img': "url('/service/theme/black/logo.png')",
    // '@ptl-header-logo-font-color': '#eeeeee', //顶部菜单左侧logo的文字颜色
    // '@primary-color': '#ee2e24',
    // '@layout-body-background': '#f0f0f0',
    // '@layout-header-color': '#bababa',
    // '@ptl-tabpage-nav-bg': '#ffffff',
    // '@ptl-tabpage-nav-color': '#5c5c5c',
    // '@ptl-tabpage-nav-active-bg': '#f0f0f0',
    // '@ant-layout-header-border-bottom': '0',
    // '@ptl-sidermenu-color': '#bababa',
    // '@ptl-sidermenu-active-color': '#ffffff',
    // '@ptl-subnav-color': '#bababa',
    // '@ptl-subnav-bg': 'rgba(49, 53, 65, 0.92)',
    // '@ptl-HomePageForTag-tagItem-color': '#1f70c6',
    // '@ptl-HomePageForTag-background': '#fafafa',
    // '@ptl-HomePageForTag-tagItem-border': '1px solid #c3d9f0',
    // '@ptl-collapsed-color': '#eeeeee',
    // 'table-header-bg': '#ee2e24',
    // 'table-header-color': '#ffffff',
    // 'table-header-bg-sm': '#ee2e24',
    // 'table-header-sort-bg': '#ee2e24',
    // 'table-header-sort-active-bg': '#ee2e24',
    'root-entry-name': 'variable',
  },
  // @ts-ignore
  title: false,
  ignoreMomentLocale: true,
  proxy: proxy[REACT_APP_ENV || 'dev'],

  // 本地配置
  define: {
    //服务基础路径
    SERVICE_BASE: '/service',
    IMG_URL: '/service',
    CORPID_DINGTALK: 'dingaba3e54383144a49',
    // LOGIN_URL: '/userAuth/omg/login',
    LOGIN_URL: '/userAuth/novonordisk/login',
  },
  manifest: {
    basePath: '/',
  },
  base: '/page',
  publicPath: '/',
  antdTheme: false, //关闭主题插件
  layout: {
    ...defaultSettings,
  },
  chunks: ['publicVenodr', 'vendors', 'umi'],
  chainWebpack: function (config, { webpack }) {
    config.merge({
      optimization: {
        splitChunks: {
          cacheGroups: {
            publicVenodr: {
              test: /(@antd|antd|@ant-design|codemirror|powerbi-client)/,
              priority: 10,
              name: 'publicVenodr',
              minChunks: 1,
              chunks: 'all',
            },
            vendors: {
              name: 'vendors',
              test: /[\\/]node_modules[\\/]/,
              priority: 1,
              minChunks: 2,
              chunks: 'async',
            },
          },
        },
      },
    });
  },
});
