import firebase from "firebase/app";
import { } from 'https://www.gstatic.com/firebasejs/9.1.0/firebase-SERVICE.js';
import { getAnalytics } from "firebase/analytics";
(function () {
    alert("aaaaaaa");
    console.log('Start file login with firebase');
    // Initialize Firebase
    const config = {
        apiKey: "AIzaSyDTWeicEyS0uRVgDZqgovD3OkREKiS7h-c",
        authDomain: "congdongchungcu-520e7.firebaseapp.com",
        projectId: "congdongchungcu-520e7",
        storageBucket: "congdongchungcu-520e7.appspot.com",
        messagingSenderId: "361964157470",
        appId: "1:361964157470:web:e093ea91bf62a8d8728c0f",
        measurementId: "G-YKT00VHDC5"
    };
    const app = firebase.initializeApp(config);
    const analytics = getAnalytics(app);

    //Google singin provider
    var ggProvider = new firebase.auth.GoogleAuthProvider();
    //Facebook singin provider
    // var fbProvider = new firebase.auth.FacebookAuthProvider();
    //Login in variables
    const btnGoogle = document.getElementById('btnGoogle');
    // const btnFaceBook = document.getElementById('btnFacebook');

    //Sing in with Google
    btnGoogle.addEventListener('click', e => {
        firebase.auth().signInWithPopup(ggProvider).then(function (result) {
            var token = result.credential.accessToken;
            var user = result.user;
            console.log('User>>Goole>>>>', user);
            userId = user.uid;

            //
            const response = await fetch("api/Firebase", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    tokenString: token,
                    userString: user,
                    userID: user.uid
                })
            });

        }).catch(function (error) {
            console.error('Error: hande error here>>>', error.code)
        })
    }, false);

    //Sing in with Facebook
    // btnFaceBook.addEventListener('click', e => {
    //     firebase.auth().signInWithPopup(fbProvider).then(function (result) {
    //         // This gives you a Facebook Access Token. You can use it to access the Facebook API.
    //         var token = result.credential.accessToken;
    //         // The signed-in user info.
    //         var user = result.user;
    //         console.log('User>>Facebook>', user);
    //         // ...
    //         userId = user.uid;
    //         //send request
    // const form = document.forms["FormEmployee"];
    // const response = await fetch("api/{controller name}", {
    //     method: "POST",
    //     headers: { "Accept": "application/json", "Content-Type": "application/json" },
    //     body: JSON.stringify({
    //         firstName: form.elements["firstName"].value,
    //         lastname: form.elements["lastname"].value,
    //         email: form.elements["email"].value
    //     })
    // });
    //     }).catch(function (error) {
    //         // Handle Errors here.
    //         var errorCode = error.code;
    //         var errorMessage = error.message;
    //         // The email of the user's account used.
    //         var email = error.email;
    //         // The firebase.auth.AuthCredential type that was used.
    //         var credential = error.credential;
    //         // ...
    //         console.error('Error: hande error here>Facebook>>', error.code)
    //     });
    // }, false)
    //jquery 
}())
