const app = Vue.createApp({
    data() {
      return {
            userId: "",
            userPassword: "",
            submitFilled: 0,
            TeacherMode: false,
            StudentMode: false,
            status: "",
            teacherUrl: "https://localhost:44308/api/Teachers/",
            studentUrl: "https://localhost:44308/api/Students/"
        };
    },
    methods: {
        toggelActiveFromStud(event)
        {
            console.log("student active");
            this.StudentMode = true;
            this.TeacherMode = false;
            this.status = "Student";

        },
        toggelActiveFromTeacher(event)
        {
            console.log("teacher active");    
            this.TeacherMode = true;
            this.StudentMode = false;
            this.status = "Teacher";

        },
        submitForm(){

            console.log(this.userId);
            console.log(this.userPassword);
            this.submitFilled++;
        },
        setId(event){
            this.userId = event.target.value;
            //console.log(this.userId);
        },
        setPass(event){
            this.userPassword = event.target.value;
            //console.log(this.userPassword);
        },

        submitClicked(){
            if (this.userId==="" && this.userPassword==="") {
                console.log("empty input");
                return;
            }
            //need to handle bad input!

            switch (this.status) {
                case "Teacher":
                    fetch(this.teacherUrl + this.userId).then((response) => {
                        if (response.ok){
                                return response.json();
                            }
                        })
                        .then((data) =>{
                            console.log(data);
                            if (data.password === this.userPassword) {
                                console.log("password match!");
                                window.location.href = 'https://localhost:44308/TeacherDashboard/TeachDash.html';
                                
                            }
                
                        }) 
                    
                    break;

                    case "Student":
                        fetch(this.studentUrl + this.userId).then((response) => {
                            if (response.ok){
                                    return response.json();
                                }
                            })
                            .then((data) =>{
                                console.log(data);
                                if (data.password === this.userPassword) {
                                    console.log("password match!");
        
                                    
                                }
                    
                            }) 
                        
                        break;
            
                default:
                    break;
            }

                  }
    },
    mounted() {
        if (localStorage.Id) {
          this.userId = localStorage.Id;
        }
        if (localStorage.password) {
            this.userPassword = localStorage.password;
            
        }
        if (localStorage.TeacherMode) {
            this.TeacherMode = localStorage.TeacherMode; 
        }
        if (localStorage.StudentMode) {
            this.StudentMode = localStorage.StudentMode; 
        }
        
      }
    ,
    watch:{
        userId(newId){
            localStorage.Id = newId;
        },
        userPassword(newPassword){
            localStorage.password = newPassword;
        },
        StudentMode(currentMode){
            localStorage.StudentMode = currentMode;
        },
        TeacherMode(currentMode){
            localStorage.TeacherMode = currentMode;
        }


    }
});

app.mount('#login');





