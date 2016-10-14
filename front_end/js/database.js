!(function(w) {
	// TODO: 模拟数据库, 这个脚本存在的意义 => 手动滑稽 
	var foodClass = [{
			"type": "北京小吃",
			"data": ["北京的焦圈", "蜜麻花", "豌豆黄", "艾窝窝", "炒肝爆肚"]
		},

		{
			"type": "上海小吃",
			"data": ["蟹壳黄", "南翔小笼馒头", "小绍兴鸡粥"]
		}, {
			"type": "天津小吃",
			"data": ["嗄巴菜", "狗不理包子", "耳朵眼炸糕", "贴饽饽熬小鱼", "棒槌果子", "桂发祥大麻花", "五香驴肉"]
		}, {
			"type": "广东小吃",
			"data": ["鸡仔饼", "皮蛋酥", "冰肉千层酥", "广东月饼", "酥皮莲蓉包", "刺猥包子", "粉果", "薄皮鲜虾饺及第粥", "玉兔饺", "干蒸蟹黄烧麦"]
		}
	];
	if (!w.localStorage.getItem('database')) w.localStorage.setItem('database', JSON.stringify(foodClass));
})(window);