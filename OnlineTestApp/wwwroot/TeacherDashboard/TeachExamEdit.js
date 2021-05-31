console.log(localStorage);
const app = Vue.createApp({
    data() {
        return {
            teacherUrl: "https://localhost:44308/api/Teachers/",
            examsUrl: "https://localhost:44308/api/Exams/",
            questionUrl: "https://localhost:44308/api/Question/",
            teacherName: "",
            userId: localStorage.currentUserId,
            userPassword: localStorage.password,
            examId: localStorage.currentExamId,
            examTitle: localStorage.currentExamTitle,
            questionArray: [],
            toggleAddNewQuestion: false,
            newQuestionToSend:{
                question: "First question",
                choices: "1;2;3;4",
                correct: "2",
                points: 30,
                examId: 2010
            }

        };
    
    },
    methods: {
        init(){
            fetch(this.teacherUrl + this.userId).then((response) => {
                if (response.ok){
                        return response.json();
                    }
                })
                .then((data) =>{
                    console.log(data);
                    this.teacherName = data.name;
                    if (data.password === this.userPassword) {
                        console.log("password match!");
                        //now need to show all his exams: GET all exam by teacher id-
                        fetch(this.questionUrl + this.examId).then((response) => {
                            if (response.ok){
                                    return response.json();
                                }
                            })
                            .then((data) =>{
                                this.questionArray.push.apply(this.questionArray, data);
                                //this.examsArray.shift();
    
                                console.log("new-logs-down");
                                console.log(this.questionArray);
                                console.log(data);
    
                                
    
    
                    
                            }) 
                        
                    }
        
                }) 

        }


    },
    mounted(){
        this.init();

        


    },
    watch:{
 
    }






});
app.mount('#exam-edit-page');
