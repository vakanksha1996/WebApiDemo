
function getAccessToken() {
    if (location.hash) {
        if (location.hash.split("access_token=")) {
            var accessToken = location.hash.split("access_token=")[1].split("&")[0];
            alert(accessToken);
            if (accessToken) {
                isUserRegistered(accessToken);

            }
        }
    }
}

function isUserRegistered(accessToken) {
    $.ajax({
        url:"/api/Account/UserInfo",
        method:"GET",
        headers:{
            'content-type':'application/JSON',
            'Authorization':'Bearer '+accessToken
        },
        success:function(response){

            if(response.HasRegistered){
                sessionStorage.setItem("tokenkey",accessToken);
                sessionStorage.setItem("userName", response.Email);
            
              window.location.href = "http://localhost:60770/Data.html";
            }
            else {
                signUpExternalUser(accessToken);

            }

        },
        error:function(){
            alert("error");
        }
    })
}

function signUpExternalUser(accessToken) {
    $.ajax({
        url: "/api/Account/RegisterExternal",
        method: "POST",
        headers: {
            'content-type': "application/JSON",
            'Authorization': "Bearer " + accessToken
        },
        success: function (response) {
            alert("success");
            window.location.href = "/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A60770%2FLogin.html&state=A2To3akBiGTXIfJd9bP5U6y1PY53It1nFeQo4ul41Gk1";
           // window.location.href = "http://localhost:60770/Data.html"
        }
        ,
        error:function(){
            alert("error");
        }
    })
}