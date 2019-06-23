﻿//front end checking
function front_end_check()
{
    var result = true;
    if (user.value == "")
    {
        user_error.innerHTML = "Your username is empty";
        result = false;
    }
    if (user_error.innerHTML != "")
        result = false;
        
    if (pwd_error.innerHTML != "")
    {
        pwd_error.innerHTML = "Your password is empty";
        result = false;
    }
    else
        pwd_error.innerHTML = "";
       
    if (email.value == "")
    {
        email_error.innerHTML = "Your email is empty";
        result = false;
    }
    else
    if (!email.value.includes("@"))
    {
        email_error.innerHTML = "Your email is not in correct format";
        result = false;
    }
    else email_error.innerHTML = "";    
    return result;
}

//POST to sumbit new entry
function submit_new_entry()
{
    if (front_end_check())
    {
        var data = {"username":user.value,
                    "password":pwd.value,
                    "firstname":fname.value,
                    "lastname":lname.value,
                    "email":email.value
                    };
            $.post("/users/submit",data, function(data){
                if (data == "0")
                {
                    location.replace("/Views/login.html");
                }
                else
                {
                    alert("Error while saving:"+data);
                }
        });
    }
}

//check if username is exist in database or not
$("#user").keyup(function(){
    $.get("/users/check/"+user.value, function(data){
                if (data == "0")
                {
                    user_error.innerHTML = "";
                }
                else
                {
                    user_error.innerHTML = "Your username've already been taken";
                }
        });
});

//check if repassword and password is the same or not
$("#re_pwd").change(function(){
    if (re_pwd.value != pwd.value)
    {
        re_error.innerHTML = "Your second password is different from first one";
    }
    else
    {
        re_error.innerHTML = "";
    }
});

$(document).ajaxStart(function(){
  $(".wait").css("display", "block");
});

$(document).ajaxComplete(function(){
  $(".wait").css("display", "none");
});