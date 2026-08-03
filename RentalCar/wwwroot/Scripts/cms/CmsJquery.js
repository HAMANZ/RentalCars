// Cookies
function getCookie(name) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function AddSubmissionToCookie(json) {
    var expiry = 10;
    var expires = new Date();
    var value = json;
    var key = "tessst";
    expires.setTime(expires.getTime() + (expiry * 24 * 60 * 60 * 1000));
    document.cookie = key + '=' + value + ';path=/' + ';expires=' + expires.toUTCString();

}

function GetSubmissionFromCookie(name,id) {
    var arr = [];
    var array = [];
    var submissions=[];
    var object = {};
    if (readCookie(name) != null) {
        readCookie(name).replace("'null'", "null");
        submissions = JSON.parse(readCookie(name));
    }
  
    $.each(submissions, function (index, value) {
        for (var item in value) {
            if (item === "SubmissionId" && value[item]===id ) {
                for (var item in value) {
                    object[item] = value[item];
                }
            }
        }

    });

    console.log(object);
    return object;
}

function AddToCookie(name,obj) {
    var submissions = [];
    if (readCookie(name) != null) {
        readCookie(name).replace("'null'", "null");
        submissions = JSON.parse(readCookie(name));
    }
    
    submissions.push(obj);
    console.log(submissions);
    createCookie(name,JSON.stringify(submissions), 30);
    return JSON.stringify(submissions);
}

const filterInPlace = (array, predicate) => {
    let end = 0;

    for (let i = 0; i < array.length; i++) {
        const obj = array[i];

        if (predicate(obj)) {
            array[end++] = obj;
        }
    }

    array.length = end;
};

function DeleteFromCookie(name,id) {
    var submissions = [];
    var array = [];
    array.push(id);
    if (readCookie(name) != null) {
        readCookie(name).replace("'null'", "null");
        submissions = JSON.parse(readCookie(name));
        const toDelete = new Set(array);
        const newArray = submissions.filter(obj => !toDelete.has(obj.Id));
        console.log("after delete");
        console.log(newArray);
        createCookie(name,JSON.stringify(newArray), 30);
        return JSON.stringify(submissions);
    }
   
}


function GetAllSubmissionFromCookie(name) {
    var submissions = [];
    var array = [];
    var object = {};
  
    if (readCookie(name) != null) {
        readCookie(name).replace("'null'", "null");
        submissions = JSON.parse(readCookie(name));
    }

    //console.log(submissions[0].FilesToUpload);

    $.each(submissions, function (index, value) {
        object = {};
        for (var item in value) {
            object[item] = value[item];
        }
        array.push(object);
    });
   
    console.log(array);
   
    return JSON.stringify(array);
}

function createCookie(name,value, days) {
    eraseCookie();
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";

    document.cookie = name+"=" + value + expires + "; path=/";
}

function eraseCookie(name) {
    document.cookie = name +'=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';

}

function readCookie(name) {
    var nameEQ = name+"=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}


function readURL(input) {
    console.log(input.files[0].name);
    $("#BackGroundImage").val(input.files[0].name);
    $("#BackgroundImage").val(input.files[0].name);
    $("#ArticleImage").val(input.files[0].name);

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
            $("#ImageBase").val(e.target.result)
        }
        reader.readAsDataURL(input.files[0]);
    }

}

function readURLBanner(input) {
    console.log(input.files[0].name);
    $("#BackGroundImageBanner").val(input.files[0].name);
    $("#BackgroundImageBanner").val(input.files[0].name);
    $("#ArticleImage").val(input.files[0].name);
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#blahBackGroundImageBanner').attr('src', e.target.result);
            $("#ImageBase").val(e.target.result)
        }
        reader.readAsDataURL(input.files[0]);
    }

}

function readURL1(input) {
    console.log(input.files[0].name);
    //$("#BackGroundImage").val(input.files[0].name);
    //$("#BackgroundImage").val(input.files[0].name);
    $("#ArticleImage").val(input.files[0].name);

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#blahBanner').attr('src', e.target.result);
            //$("#BannerImages").val(e.target.result)
        }
        reader.readAsDataURL(input.files[0]);
    }

}

$(document).ready(function () {

    $("#imgInp").change(function () {
        readURL(this);
    });

    $("#imgInpBanner").change(function () {
        readURLBanner(this);
    });
    //var editor = "";

    $(".editArticle").on("click", function (event) {
        event.preventDefault();
        var Id = $("#Id").val();
        var Key = $(this).attr("data-row-type");
        var Value = $(this).attr("data-row-value");
        //console.log(Id);
        //console.log(Key);
        //console.log(Value.replace(/</g, '*').replace(/>/g, '#'));

        $.ajax({
            url: '/Admin/Edit2',
            type: "POST",
            data: { Id: Id, Key: Key, Value: Value.replace(/</g, '*').replace(/>/g, '#') },
        }).done(function (result) {
            console.log(result);
            $("#EditPageModal .modal-body").html(result);
            $('#EditPageModal').modal('show');
            $('#summernote').summernote();
            //editor = CKEDITOR.replace('ckeditor', { height: '380px', startupFocus: true });
        });
    });

    $("#SaveBtn").on("click", function () {
        //console.log("clicked");
        //console.log($("#Key").val());
        //console.log(editor.getData());
        var key = $("#Key").val();
        console.log(key);
        var html = $('#summernote').summernote('code');
        console.log(html);
        //$("." + key).html(editor.getData());
        //$("." + key).val(editor.getData());

        $("." + key).html(html);
        $("." + key).val(html);
        $('#EditPageModal').modal('hide');
    });

    $("#BannerPublish").on("click", function () {
        console.log("Banner Publish Clicked");
        //Title
        var Title = $("input[name=Title]").val();
        if (Title != null) {
            $("input[name=Title]").val(Title.replace(/</g, '*').replace(/>/g, '#'));
        }
        //SubTitle
        var SubTitle = $("input[name=SubTitle]").val();
        if (SubTitle != null) {
            $("input[name=SubTitle]").val(SubTitle.replace(/</g, '*').replace(/>/g, '#'));
        }
        //Sentence
        var Sentence = $("input[name=Sentence]").val();
        if (Sentence != null) {
            $("input[name=Sentence]").val(Sentence.replace(/</g, '*').replace(/>/g, '#'));
        }
        //Description
        var Description = $("input[name=Description]").val();
        if (Description != null) {
            $("input[name=Description]").val(Description.replace(/</g, '*').replace(/>/g, '#'));
        }
        //FullDescription
        var FullDescription = $("input[name=FullDescription]").val();
        if (FullDescription != null) {
            $("input[name=FullDescription]").val(FullDescription.replace(/</g, '*').replace(/>/g, '#'));
        }


        //ArticleTitle
        var ArticleTitle = $("input[name=ArticleTitle]").val();
        if (ArticleTitle != null) {
            $("input[name=ArticleTitle]").val(ArticleTitle.replace(/</g, '*').replace(/>/g, '#'));
        }
        //ArticleDescription
        var ArticleDescription = $("input[name=ArticleDescription]").val();
        if (ArticleDescription != null) {
            $("input[name=ArticleDescription]").val(ArticleDescription.replace(/</g, '*').replace(/>/g, '#'));
        }
        //ArticleFullDescription
        var ArticleFullDescription = $("input[name=ArticleFullDescription]").val();
        if (ArticleFullDescription != null) {
            $("input[name=ArticleFullDescription]").val(ArticleFullDescription.replace(/</g, '*').replace(/>/g, '#'));
        }


        //BannerTitle
        var BannerTitle = $("input[name=BannerTitle]").val();
        if (BannerTitle != null) {
            $("input[name=BannerTitle]").val(BannerTitle.replace(/</g, '*').replace(/>/g, '#'));
        }
        //BannerDescription
        var BannerDescription = $("input[name=BannerDescription]").val();
        if (BannerDescription != null) {
            $("input[name=BannerDescription]").val(BannerDescription.replace(/</g, '*').replace(/>/g, '#'));
        }

        //console.log("Title" + $("input[name=Title]").val());
        //console.log("SubTitle"+$("input[name=SubTitle]").val());
        //console.log("Sentence" + $("input[name=Sentence]").val());
        //console.log("Description" + $("input[name=Description]").val());
        //BannerForm

        $(".BannerForm").submit();


    });
});