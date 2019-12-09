//包裹搜索控制
$(function () {
	$("#attention1").mouseover(function(){
		$(".attention").show();
	});
	$("#attention1").mouseout(function(){
		$(".attention").hide();
	});
    $("#ddl_country").change(function () {
        var country = $(this).val();
        var city1 = $("#ddl_city1");
        $("#ddl_city1 option").remove();
        $("#ddl_city2").hide();
        switch(country)
        {
            case "越南":
                city1.append("<option value='请选择'>请选择</option>");
                city1.append("<option value='越南南部'>越南南部</option>");
                city1.append("<option value='越南中部'>越南中部</option>");
                city1.append("<option value='越南北部'>越南北部</option>");
                break;
            case "老挝":
                city1.append("<option value='请选择'>请选择</option>");
                city1.append("<option value='万象'>万象</option>");
                city1.append("<option value='琅勃拉邦'>琅勃拉邦</option>");
                break;
            case "泰国":
                city1.append("<option value='请选择'>请选择</option>");
                city1.append("<option value='曼谷'>曼谷</option>");
                city1.append("<option value='清迈'>清迈</option>");
                city1.append("<option value='普吉'>普吉</option>");
                city1.append("<option value='芭提雅'>芭提雅</option>");
                break;
            case "柬埔寨":
                city1.append("<option value='请选择'>请选择</option>");
                city1.append("<option value='金边'>金边</option>");
                city1.append("<option value='吴哥窟'>吴哥窟</option>");
                city1.append("<option value='西哈努市'>西哈努市</option>");
                city1.append("<option value='暹粒'>暹粒</option>");
                break;
            case "缅甸":
                city1.append("<option value='请选择'>请选择</option>");
                city1.append("<option value='曼德勒'>曼德勒</option>");
                city1.append("<option value='仰光'>仰光</option>");
                break;
            default:
                city1.append("<option value='请选择'>请选择</option>");
                break;
        }
    });
    $("#ddl_city1").change(function () {
        var country = $(this).val();
        var city2 = $("#ddl_city2");
        $("#ddl_city2 option").remove();
        switch (country)
        {
            case "越南南部":
                city2.append("<option value='请选择'>请选择</option>");
                city2.append("<option value='胡志明市及周边市郊工业区'>胡志明市及周边市郊工业区</option>");
                city2.append("<option value='平阳省'>平阳省</option>");
                city2.append("<option value='同奈省'>同奈省</option>");
                city2.show();
                break;
            case "越南中部":
                city2.append("<option value='请选择'>请选择</option>");
                city2.append("<option value='义安市'>义安市</option>");
                city2.append("<option value='平定市'>平定市</option>");
                city2.append("<option value='顺化市'>顺化市</option>");
                city2.append("<option value='河群省'>河群省</option>");
                city2.append("<option value='广义省'>广义省</option>");
                city2.append("<option value='广南省'>广南省</option>");
                city2.append("<option value='昆嵩省'>昆嵩省</option>");
                city2.show();
                break;
            case "越南北部":
                city2.append("<option value='请选择'>请选择</option>");
                city2.append("<option value='河内市'>河内市</option>");
                city2.append("<option value='海防市'>海防市</option>");
                city2.append("<option value='海洋市'>海洋市</option>");
                city2.append("<option value='兴安市'>兴安市</option>");
                city2.append("<option value='北宁市'>北宁市</option>");
                city2.append("<option value='江北市'>江北市</option>");
                city2.append("<option value='太原市'>太原市</option>");
                city2.show();
                break;
            default:
				city2.hide();
                break;
        }
    });
    $("#component-1").click(function () {
        var city = null;
        var kg = $("#tb_goodsKg").val();
        var result = 0;
        if ($("#ddl_city1").val() != "请选择") {
            if(kg!=null && kg!= ""){
                if ($("#ddl_city2 option").size() == 0) {
                    city = $("#ddl_city1").val();
                }
                else {
                    city = $("#ddl_city2").val();
                }
                switch (city) {
                    case "胡志明市及周边市郊工业区":
                        result = CalculatingPrice(kg);
                        break;
                    case "平阳省":
                        result = CalculatingPrice(kg);
                        break;
                    case "同奈省":
                        result = CalculatingPrice(kg);
                        break;
                    case "义安市":
                        result = CalculatingPrice(kg);
                        break;
                    case "平定市":
                        result = CalculatingPrice(kg);
                        break;
                    case "顺化市":
                        result = CalculatingPrice(kg);
                        break;
                    case "河群省":
                        result = CalculatingPrice(kg);
                        break;
                    case "广义省":
                        result = CalculatingPrice(kg);
                        break;
                    case "广南省":
                        result = CalculatingPrice(kg);
                        break;
                    case "昆嵩省":
                        result = CalculatingPrice(kg);
                        break;
                    case "兴安市":
                        result = CalculatingPrice1(kg);
                        break;
                    case "北宁市":
                        result = CalculatingPrice1(kg);
                        break;
                    case "江北市":
                        result = CalculatingPrice1(kg);
                        break;
                    case "太原市":
                        result = CalculatingPrice1(kg);
                        break;
                    case "河内市":
                        result = CalculatingPrice2(kg);
                        break;
                    case "海防市":
                        result = CalculatingPrice2(kg);
                        break;
                    case "海洋市":
                        result = CalculatingPrice2(kg);
                        break;
                    case "万象":
                        result = CalculatingPrice3(kg);
                        break;
                    case "琅勃拉邦":
                        result = CalculatingPrice3(kg);
                        break;
                    case "曼谷":
                        result = CalculatingPrice4(kg);
                        break;
                    case "清迈":
                        result = CalculatingPrice4(kg);
                        break;
                    case "普吉":
                        result = CalculatingPrice4(kg);
                        break;
                    case "芭提雅":
                        result = CalculatingPrice4(kg);
                        break;
                    case "金边":
                        result = CalculatingPrice6(kg);
                        break;
                    case "吴哥窟":
                        result = CalculatingPrice6(kg);
                        break;
                    case "西哈努市":
                        result = CalculatingPrice6(kg);
                        break;
                    case "暹粒":
                        result = CalculatingPrice6(kg);
                        break;
                    case "曼德勒":
                        result = CalculatingPrice5(kg);
                        break;
                    case "仰光":
                        result = CalculatingPrice5(kg);
                        break;
					case "请选择":
                        result = -1;
                        break;
                    default:
                        result = 0;
                        break;
                }
                if (result == 0) {
                    alert("目的地为越南的货物最低50kg,其他必须最低75kg");
                }
                else
                {
					if (result == -1) {
						alert("请选择城市！");
					}
					else
					{
						$(".P_main_b h1").fadeOut();
						setTimeout(function(){
							$("#result").html(result);
							$(".P_main_b h1").fadeIn();
							},1000);
						
					}
                }
            }
            else
            {
                alert("请输入货物公斤!");
            }
        }
        else
        {
            alert("请先选择城市!");
        }
    });
    function CalculatingPrice(a)
    {
        var result1 = 0;
        if (50 < a && a < 101) {
            result1 = 11 * a;
        }
        if (101< a && a < 301) {
            result1 = 10 * a;
        }
        if (301< a && a < 501) {
            result1 = 9 * a;
        }
        if (501< a && a < 1001) {
            result1 = 8 * a;
        }
        if (1001< a && a < 3001) {
            result1 = 7.5 * a;
        }
        if (a > 3001) {
            result1 = 6 * a;
        }
        return result1;
    }
    function CalculatingPrice1(a) {
        var result1 = 0;
        if (50 < a && a < 101) {
            result1 = 10 * a;
        }
        if (101< a && a < 301) {
            result1 = 9 * a;
        }
        if (301< a && a < 501) {
            result1 = 8 * a;
        }
        if (501< a && a < 1001) {
            result1 = 7 * a;
        }
        if (1001< a && a < 3001) {
            result1 = 6 * a;
        }
        if (a > 3001) {
            result1 = 5 * a;
        }
        return result1;
    }
    function CalculatingPrice2(a) {
        var result1 = 0;
        if (50 < a && a < 101) {
            result1 = 8.5 * a;
        }
        if (101< a && a < 301) {
            result1 = 7.5 * a;
        }
        if (301< a && a < 501) {
            result1 = 6.5 * a;
        }
        if (501< a && a < 1001) {
            result1 = 5.5 * a;
        }
        if (1001< a && a < 3001) {
            result1 = 5 * a;
        }
        if (a > 3001) {
            result1 = 4.5 * a;
        }
        return result1;
    }
    function CalculatingPrice3(a) {
        var result1 = 0;
        if (75 < a && a < 150) {
            result1 = 10 * a;
        }
        if (150 < a && a < 301) {
            result1 = 9.5 * a;
        }
        if (301< a && a < 501) {
            result1 = 7.5 * a;
        }
        if (501< a && a < 1001) {
            result1 = 6 * a;
        }
        if (1001< a && a < 3001) {
            result1 = 5.5 * a;
        }
        if (a > 3001) {
            result1 = 5 * a;
        }
        return result1;
    }
    function CalculatingPrice4(a) {
        var result1 = 0;
        if (75 < a && a < 150) {
            result1 = 12 * a;
        }
        if (150 < a && a < 301) {
            result1 = 10.5 * a;
        }
        if (301< a && a < 501) {
            result1 = 8.5 * a;
        }
        if (501< a && a < 1001) {
            result1 = 7 * a;
        }
        if (1001< a && a < 3001) {
            result1 = 7 * a;
        }
        if (a > 3001) {
            result1 = 6.5 * a;
        }
        return result1;
    }
    function CalculatingPrice5(a) {
        var result1 = 0;
        if (75 < a && a < 150) {
            result1 = 10 * a;
        }
        if (150 < a && a < 301) {
            result1 = 8.5 * a;
        }
        if (301< a && a < 501) {
            result1 = 8 * a;
        }
        if (501< a && a < 1001) {
            result1 = 7.5 * a;
        }
        if (1001< a && a < 3001) {
            result1 = 7 * a;
        }
        if (a > 3001) {
            result1 = 5 * a;
        }
        return result1;
    }
    function CalculatingPrice6(a) {
        var result1 = 0;
        if (75 < a && a < 150) {
            result1 = 12 * a;
        }
        if (150 < a && a < 301) {
            result1 = 9.5 * a;
        }
        if (301< a && a < 501) {
            result1 = 9 * a;
        }
        if (501< a && a < 1001) {
            result1 = 8.5 * a;
        }
        if (1001< a && a < 3001) {
            result1 = 8 * a;
        }
        if (a > 3001) {
            result1 = 7.5 * a;
        }
        return result1;
    }
})
//包裹搜索控制


//head下拉的时候跟随移动控制
$(function () {

    var winHeight = $(document).scrollTop();
    var scrollY = $(document).scrollTop();// 获取垂直滚动的距离，即滚动了多少

    if (scrollY > 100) { //如果滚动距离大于550px则隐藏，否则删除隐藏类
        $('.navitem').addClass('hiddened');
    }
    else {
        $('.navitem').removeClass('hiddened');
    }
    $(window).scroll(function () {
        var scrollY = $(document).scrollTop();// 获取垂直滚动的距离，即滚动了多少

        if (scrollY > 100) { //如果滚动距离大于550px则隐藏，否则删除隐藏类
            $('.navitem').addClass('hiddened');
        }
        else {
            $('.navitem').removeClass('hiddened');
        }


    });
});
//head下拉的时候跟随移动控制



//banner控制
var animateTime = 800//图片滚动时间
var fadeTime = 400//文字渐隐显示时间
var lineTime = 6000//图片切换时间
var maxPage = 6;//图片张数
var picIndex = 1;
var locked = 0;

window.onload = function () {
    setTimeout(function () {
        $(".tp-banner li").css("display", "block");
    }, 1000);

}
$(function () {
    lineSwitch(lineTime, animateTime)
    $('.tp-leftarrow').click(function () {
        if (locked != 1) {
            picIndex--
            showPicByIndexL(picIndex)
            $('.tp-bannertimer').css('width', 0);
        }
        else {
            picIndex = 1
            showPicByIndexR(picIndex)
            $('.tp-bannertimer').css('width', 0);
        }
    })
    $('.tp-rightarrow').click(function () {
        if (locked != 1) {
            picIndex++
            showPicByIndexR(picIndex)
            $('.tp-bannertimer').css('width', 0);
        }
        else {
            picIndex = 1
            showPicByIndexR(picIndex)
            $('.tp-bannertimer').css('width', 0);
        }

    })
});

function showPicByIndexR(n) {
    if (n < 1) {
        n = maxPage
    } else if (n > maxPage) {
        n = 1;
    }
    picIndex = n;
    locked = 1

    $(".tp-banner li").eq(n - 1).siblings().css('z-index', '42');
    $(".tp-banner li").eq(n - 2).css('z-index', '43');
    $(".tp-banner li").eq(n - 1).css('z-index', '44');
    $(".tp-banner li").eq(n - 1).css('left', '100%')
    $(".tp-banner li").eq(n - 1).css('right', '-100%')

    $(".tp-banner li").eq(n - 1).animate({ right: '0', left: '0' }, animateTime, function () {


        $(".tp-banner li").eq(n - 1).find('.ad-info-txt').show();
        $(".tp-banner li").eq(n - 1).find('.ad-details').show();
        $(".tp-banner li").eq(n - 1).find('.ad_detal_btn').show();
        $(".tp-banner li").eq(n - 1).find('.ad_detal_tips').show();

        $(".tp-banner li").eq(n - 1).siblings().find('.ad-info-txt').hide();
        $(".tp-banner li").eq(n - 1).siblings().find('.ad-details').hide();
        $(".tp-banner li").eq(n - 1).siblings().find('.ad_detal_btn').hide();
        $(".tp-banner li").eq(n - 1).siblings().find('.ad_detal_tips').hide();
        $(".tp-banner li").eq(n - 1).siblings().css('right', '-100%');
        locked = 0
    });
}

function showPicByIndexL(n) {
    if (n < 1) {
        n = maxPage
    } else if (n > maxPage) {
        n = 1;
    }
    picIndex = n;
    locked = 1
    $(".tp-banner li").eq(n - 1).siblings().css('z-index', '42');
    $(".tp-banner li").eq(n).css('z-index', '43');
    $(".tp-banner li").eq(n - 1).css('z-index', '44');
    $(".tp-banner li").eq(n - 1).css('left', '-100%')
    $(".tp-banner li").eq(n - 1).css('right', '0')

    $(".tp-banner li").eq(n - 1).animate({ left: '0', left: '0' }, animateTime, function () {
        $(".tp-banner li").eq(n - 1).find('.ad-info-txt').show();
        $(".tp-banner li").eq(n - 1).find('.ad-details').show();
        $(".tp-banner li").eq(n - 1).find('.ad_detal_btn').show();
        $(".tp-banner li").eq(n - 1).find('.ad_detal_tips').show();


        $(".tp-banner li").eq(n - 1).siblings().find('.ad-info-txt').hide();
        $(".tp-banner li").eq(n - 1).siblings().find('.ad-details').hide();
        $(".tp-banner li").eq(n - 1).siblings().find('.ad_detal_btn').hide();
        $(".tp-banner li").eq(n - 1).siblings().find('.ad_detal_tips').hide();
        $(".tp-banner li").eq(n - 1).siblings().css('left', '-100%');
        locked = 0
    });
}

function lineSwitch(lineTime) {

    $('.tp-bannertimer').animate({ width: '100%' }, lineTime, 'linear', function () {
        picIndex++
        showPicByIndexR(picIndex)
        $('.tp-bannertimer').css('width', 0);
        setTimeout(function () {
            lineSwitch(lineTime);
        }, animateTime + 200)

    })
}
//banner控制

//首页语言控制
var language = new Array();
$(function () {
    var str = null;
    $.ajax({
        url: '/json.txt',
        dataType: 'text',
        success: function (data) {
            str = data;
            language = str.split('|');//注split可以用字符或字符串分割
        }
    });
});
var languageing = false;
$(function () {
    $("#language").click(function () {
        if (languageing) {
            location.reload();
        }
        if (!languageing) {
            $(".navitem_logo a img").attr("src", "/image/logoE.png");
            $(".topbar_right_list>li:nth-child(1) span").html(language[0]);
            $(".topbar_right_list>li:nth-child(3) span").html(language[1]);
            $(".topbar_right_list>li:nth-child(5) span").html(language[13]);
            for (var i = 2; i < 14; i++) {
                $(".drop_down a").eq(i - 2).html(language[i]);
            }
            var o = 14;
            for (var i = 0; i <= 6; i = i + 2) {
                $(".tp-caption").eq(i).html(language[o]);
                o++;
            }
            o = 18;
            for (var i = 0; i <= 8; i++) {
                $(".tp-caption p").eq(i).html(language[o]);
                o++
            }
            $(".OpenEyes_img span").html(language[26]);
            $(".P_main>header>h").html(language[27]);
            $("#attention1 font").html(language[28]);
            $(".P_main_1>span").html(language[29]);
            $("#ddl_country>option:first-child").html(language[30]);
            $("#ddl_city1>option:first-child").html(language[30]);
            $(".P_main_2>span:first-child").html(language[31]);
            $(".P_main_b>h1>span:first-child").html(language[32]);
            $("#component-1>font").html(language[33]);
            o = 34;
            for (var i = 0; i <= 3; i++) {
                $(".tltle_bar>h3").eq(i).html(language[o]);
                o++
            }
            $(".sp>h1>font").html(language[38]);
            $(".sp>h1>span>a").html(language[39]);
            o = 40;
            for (var i = 0; i <= 8; i++) {
                $(".sp>ul>li>a>h1").eq(i).html(language[o]);
                $(".sp>ul>li>a>h1").eq(i).css("font-size","1.2em");
                o++
            }
            $(".more_product a").html(language[48]);
            o = 49;
            for (var i = 0; i <= 3; i++) {
                $(".Our_products_list_info h3").eq(i).html(language[o+i]);;
            }
            o = 52;
            for (var i = 0; i <= 6; i = i+2) {
                $(".Our_products_list_info p").eq(i).html(language[o]);
                o++;
            }
            for (var i = 0; i <= 3; i++) {
                $(".Our_products_list_info .detail_btn a").eq(i).html(language[55]);
            }
            $(".partner_item_list li").css("width", "50%");
            o = 57;
            for (var i = 0; i <= 5; i++) {
                $("#hzhb>li a span").eq(i).html(language[o+i]);
            }
            $(".partner_item_list h4").html(language[56]);
            $(".partner_pt h4").html(language[61]);
            $(".foot_bottom span").eq(0).html(language[62]);
            $(".foot_bottom span").eq(1).html(language[63]);
            
            $(".foot_logo img").attr("src", "/image/logoE.png");
            o = 64;
            for (var i = 0; i <= 8; i++) {
                $(".attention p").eq(i).html(language[o+i]);
            }
            $(".topbar_left font").html(language[72]);
            o = 73;
            for (var i = 0; i <= 2; i++) {
                $(".fb span").eq(i).html(language[o + i]);
            }
            $(".fb img").attr("alt", language[76]);
            $(".submit .btn-1").html(language[0]);
            $(".submit a").eq(0).html(language[1]);
            $(".submit a").eq(1).html(language[77]);
            $(".latestlogin strong").html(language[78]);
            languageing = true;
        }
    })
});

//首页语言控制

var tims = 0;
var Jump = '';
function closeTime() {
    $("#msg").fadeOut();
    if (Jump != '')
    {
        window.location.replace(Jump);
    }
    tims = 0;
}


/**
 *执行失败消息提示框
 */
function errorMsg(message, link) {
    $("#msg section p").html(message);
    $("#msg").fadeIn();
    setTimeout(function () {
        $("#msg").fadeOut();
    }, 2000);
}
/**
 *执行成功消息提示框
 */
function succeedMsg(message) {
    if (Jump != '') {
        $("#msg section p").html(message + ",3秒后跳转");
    }
    else
    {
        $("#msg section p").html(message);
    }
    $("#msg").fadeIn();
}
/**
 * 自动关闭弹框
 * s 剩余秒数
 * link 跳转链接
 */
function succeedAutomaticClose() {
    if (time == 0) {
        $("#msg").fadeOut();
        if (Jump != '')
        {
            window.location.replace(Jump);
        }
        return false;
    }
    time--;
    setTimeout('succeedAutomaticClose()', 1000);
}

/**
 * 数据提交成功提示框
 * error 是否是错误信息，true是  false不是
 * link 跳转链接 默认为空，为空则不跳转
 */
function message(error, message, link) {
    time = 0;
    Jump = link;
    if (error) {
        errorMsg(message);
    } else {
        succeedMsg(message);
        time = 4;
        succeedAutomaticClose();
    }
}

function _show() {
    $("#loading").show();
}
function _hide() {
    $("#loading").hide();
}