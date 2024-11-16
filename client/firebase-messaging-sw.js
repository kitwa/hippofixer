importScripts('https://www.gstatic.com/firebasejs/9.1.3/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.1.3/firebase-messaging-compat.js');

firebase.initializeApp({
    apiKey: "AIzaSyAs2-Ydrvkaz0M8QeFtXrGJf9hcQa2VAYs",
    authDomain: "hippofixer.firebaseapp.com",
    projectId: "hippofixer",
    storageBucket: "hippofixer.firebasestorage.app",
    messagingSenderId: "1026023552483",
    appId: "1:1026023552483:web:904a469726ec24cce45cd6",
  });

const messaging = firebase.messaging();

messaging.onBackgroundMessage((payload) => {
  console.log('Received background message ', payload);
  const notificationTitle = payload.notification.title;
  const notificationOptions = {
    body: payload.notification.body,
    icon: '/assets/icons/icon-192x192.png'
  };

  self.registration.showNotification(notificationTitle, notificationOptions);
});
