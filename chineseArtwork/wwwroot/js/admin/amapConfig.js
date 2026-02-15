var amapCommonConfigList = {
    gaodeCom: {
      company: "高德",
      mapCode: "GS(2025)5996号",
      jiace: "甲测资字11112528",
      beian:
        "<a href='https://beian.miit.gov.cn' target='_blank'>京ICP备2023020281号-13</a>",
      anbei: "",
      netMedicine:"",
      jubao: "",
      satelliteCode: "GS(2026)0185号 - Image© DigitalGlobe＆spaceview",
      dianxinxuke:
        "<a href='https://cache.gaode.com/activity/lowcode/h5/qzKxxus1/index.html' target='_blank'>增值电信业务经营许可证</a>",
      zhizhao:
        "<a href='https://zzlz.gsxt.gov.cn/businessCheck/verifKey.do?showType=p&serial=911101147263767522-SAIC_SHOW_1000009111011472637675221634549375904&signData=MEYCIQC6YhrawPfPQ8XfjPY8fN5wNdu8l+KqdSItU2DkYJ/JFwIhANcn/1OtIQWvr1tXIV8oTPoiUNj32JVBn4bpDsXhNZ8d' target='_blank'><img src='https://zzlz.gsxt.gov.cn/images/lz4.png'></a>",
    },
    amapCom: {
      company: "高德",
      mapCode: "GS(2025)5996号",
      jiace: "甲测资字11112528",
      beian:
        "<a href='https://beian.miit.gov.cn' target='_blank'>京ICP备2023020281号-12</a>",
      anbei:
        " - <span><a target='_blank' style='color:#565656' href='//beian.mps.gov.cn/#/query/webSearch?code=11010502056787'><img style='margin-bottom:1px' src='/assets/img/beian.png' />京公网安备 11010502056787号</a></span>",
      netMedicine:"",
      jubao: "",
      satelliteCode: "GS(2026)0185号 - Image© DigitalGlobe＆spaceview",
      dianxinxuke:
        "<a href='https://cache.gaode.com/activity/lowcode/h5/qzKxxus1/index.html' target='_blank'>增值电信业务经营许可证</a>",
      zhizhao:
        "<a href='https://zzlz.gsxt.gov.cn/businessCheck/verifKey.do?showType=p&serial=911101147263767522-SAIC_SHOW_1000009111011472637675221634549375904&signData=MEYCIQC6YhrawPfPQ8XfjPY8fN5wNdu8l+KqdSItU2DkYJ/JFwIhANcn/1OtIQWvr1tXIV8oTPoiUNj32JVBn4bpDsXhNZ8d' target='_blank'><img src='https://zzlz.gsxt.gov.cn/images/lz4.png'></a>",
    },
    amapCn: {
      company: "高德",
      mapCode: "GS(2025)5996号",
      jiace: "甲测资字11112528",
      beian:
        "<a href='https://beian.miit.gov.cn/#/Integrated/index' target='_blank'>京ICP备2022004888号-1</a>",
      netMedicine:
        "<a href='https://beian.miit.gov.cn/#/Integrated/index' target='_blank'>(京)网药械信息备字（2024）第00336号</a> | ",
      anbei: "",
      jubao: "",
      satelliteCode: "GS(2026)0185号 - Image© DigitalGlobe＆spaceview",
      dianxinxuke:
        "<a href='https://cache.gaode.com/activity/lowcode/h5/qzKxxus1/index.html' target='_blank'>增值电信业务经营许可证</a>",
      zhizhao:
        "<a href='https://zzlz.gsxt.gov.cn/businessCheck/verifKey.do?showType=p&serial=911101147263767522-SAIC_SHOW_1000009111011472637675221634549375904&signData=MEYCIQC6YhrawPfPQ8XfjPY8fN5wNdu8l+KqdSItU2DkYJ/JFwIhANcn/1OtIQWvr1tXIV8oTPoiUNj32JVBn4bpDsXhNZ8d' target='_blank'><img src='https://zzlz.gsxt.gov.cn/images/lz4.png'></a>",
    },
    amapNet: {
      company: "高德",
      mapCode: "GS(2025)5996号",
      jiace: "甲测资字11112528",
      beian:
        "<a href='https://beian.miit.gov.cn/#/Integrated/index' target='_blank'>京ICP备2023020281号-1</a>",
      netMedicine:"",
      anbei: "",
      jubao: "",
      satelliteCode: "GS(2026)0185号 - Image© DigitalGlobe＆spaceview",
      dianxinxuke:
        "<a href='https://cache.gaode.com/activity/lowcode/h5/qzKxxus1/index.html' target='_blank'>增值电信业务经营许可证</a>",
      zhizhao:
        "<a href='https://zzlz.gsxt.gov.cn/businessCheck/verifKey.do?showType=p&serial=911101147263767522-SAIC_SHOW_1000009111011472637675221634549375904&signData=MEYCIQC6YhrawPfPQ8XfjPY8fN5wNdu8l+KqdSItU2DkYJ/JFwIhANcn/1OtIQWvr1tXIV8oTPoiUNj32JVBn4bpDsXhNZ8d' target='_blank'><img src='https://zzlz.gsxt.gov.cn/images/lz4.png'></a>",
    },
  };
  
  // var _host = window.location.href; // 测试阶段使用
  var _host = window.location.host;
  var amapCommonConfig = _host.includes("amap.net")
    ? amapCommonConfigList.amapNet
    : _host.includes("amap.cn")
    ? amapCommonConfigList.amapCn
    : _host.includes("gaode.com")
    ? amapCommonConfigList.gaodeCom
    : amapCommonConfigList.amapCom;
  
  var locSpecial = [{
    lng: [-106.546527, -106.523406],
    lat: [38.786244, 38.803914],
    replace: [116.489717, 40.00258],
    visible: true
  }];
  
  amapCommonConfig.links = [
    {
      title: "资质证照",
      url: "https://a.amap.com/pc/static/page/info.html",
      trace: "zizhi",
    },
    {
      title: "协议与声明",
      url: "https://map.amap.com/doc/serviceitem.html",
      trace: "tos",
    },
    {
      title: "开放平台",
      url: "https://lbs.amap.com/",
      trace: "lbs",
    },
    {
      title: "新增地点",
      url: "https://map.amap.com/help/index.html?type=addpoi",
      trace: "addpoi",
    },
    {
      title: "意见反馈",
      url: "https://map.amap.com/help/index.html",
      trace: "feedback",
    },
    {
      title: "商户免费标注",
      url: "http://bgc.amap.com?src=pcbottom",
      trace: "addtag",
    },
    {
      title: "车机版",
      url: "https://www.amapauto.com?src=pc_openapi",
      trace: "auto",
    },
    {
      title: "网上有害信息举报",
      url: "https://www.12377.cn/",
      trace: "tipoff",
    },
    {
      title: "涉未成年人举报",
      url: "https://a.amap.com/pc/static/page/report.html",
      trace: "tipoff_wcn",
    },
    {
      title: "算法推荐举报",
      url: "https://a.amap.com/pc/static/page/report.html",
      trace: "tipoff_sftj",
    },
    {
      title: "生活服务专项举报",
      url: "https://a.amap.com/pc/static/page/report.html",
      trace: "tipoff_shfwzx",
    },
    {
      title: "廉正举报",
      url: "https://a.amap.com/pc/static/page/report.html",
      trace: "tipoff_lz",
    },
  ];
  
  var messageLoginAgreement = '我已阅读并同意 <a target="_blank" href="https://terms.alicdn.com/legal-agreement/terms/c_platform_service_agreement/20250314165704167/20250314165704167.html">《高德服务条款》</a><a target="_blank" href="https://terms.alicdn.com/legal-agreement/terms/privacy_policy_full/20250314145608599/20250314145608599.html">《高德地图基本功能隐私政策》</a><a target="_blank" href="https://terms.alicdn.com/legal-agreement/terms/privacy_other/20250313232240407/20250313232240407.html">《高德账号服务个人信息处理规则》</a>；未注册的手机号将自动注册';