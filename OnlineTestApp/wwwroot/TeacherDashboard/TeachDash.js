const app = Vue.createApp({
    data() {
        return {
            teacherUrl: "https://localhost:44308/api/Teachers/",
            examsUrl: "https://localhost:44308/api/Exams/",
            teacherName: "",
            userId: localStorage.Id,
            userPassword: localStorage.password,
            examsArray: [],
            toggleAddNewTest: false,
            newExamToSend:{
                title:"",
                teacherId: this.userId,
                dateOfTest: "",
                durationOfTest: 0,
                testHour: ""
            }

        };
    
    },
    methods: {
        submitClickedNewTeacher(){
            this.newExamToSend.dateOfTest = this.newExamToSend.dateOfTest.slice(0,10);
            this.newExamToSend.dateOfTest += ("T" + this.newExamToSend.testHour+":00.000Z");
           
            fetch('https://localhost:44308/api/Exams',{
                method: 'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify( {
                    title: this.newExamToSend.title,
                    teacherId: this.userId,
                    dateOfTest: this.newExamToSend.dateOfTest,
                    durationOfTest: this.newExamToSend.durationOfTest

                })

            });
            this.toggleAddNewTest= !this.toggleAddNewTest;
            console.log(this.newExamToSend);
            /* fetch(this.teacherUrl + this.userId).then((response) => {
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
                        fetch(this.examsUrl + this.userId).then((response) => {
                            if (response.ok){
                                    return response.json();
                                }
                            })
                            .then((data) =>{
                                
                                
                                var examTemp={
                                    id: "",
                                    title: "",
                                    teacherId: "",
                                    dateOfTest: ""
                                }
                                var i = 0;
                                this.examsArray.push.apply(this.examsArray, data);
                                this.examsArray.shift();
    
                                //console.log("new-logs-down");
                                //console.log(this.examsArray);
                                console.log("fetched new list");
    
                                
    
    
                    
                            }) 
                        
                    }
        
                })  */

        },
        setDateOfTest(event){
            
            this.newExamToSend.dateOfTest = event.target.value;
            
        },
        setTimeOfTest(event){
            console.log(this.newExamToSend.dateOfTest);
            
            this.newExamToSend.testHour = event.target.value ;
            
        },
        setTitleOfTest(event){
            this.newExamToSend.title = event.target.value;
        },
        setTeacherIdOfTest(event){
            this.newExamToSend.teacherId = event.target.value;
        },
        setDurationOfTest(event){
            this.newExamToSend.durationOfTest = event.target.value;
        }




    },
    mounted(){
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
                    fetch(this.examsUrl + this.userId).then((response) => {
                        if (response.ok){
                                return response.json();
                            }
                        })
                        .then((data) =>{
                            
                            
                            var examTemp={
                                id: "",
                                title: "",
                                teacherId: "",
                                dateOfTest: ""
                            }
                            var i = 0;
                            this.examsArray.push.apply(this.examsArray, data);
                            this.examsArray.shift();

                            console.log("new-logs-down");
                            console.log(this.examsArray);
                            console.log(data);

                            


                
                        }) 
                    
                }
    
            }) 


    },
    watch:{}






});
app.component('exam-list-item',{
    props:['exam'],
    template:`<li class="d-flex justify-content-between shadow">
    <div class="d-flex flex-row align-items-center"><i class="fa fa-check-circle checkicon"></i>
        <div class="ml-2">
            <h6 class="mb-0">{{ exam.title }}</h6>
            <div class="d-flex flex-row mt-1 text-black-50 date-time">
                <div><i class="fa fa-calendar-o"></i><span class="ml-2"> {{exam.dateOfTest}}   22 May 2020 11:30 PM</span></div>
                
            </div>
        </div>
    </div>
    <div class="d-flex flex-row align-items-center">
        <div class="d-flex flex-column mr-2">
            <div class="profile-image"> <button type="button" class="btn btn-outline-secondary"><img class="" src="/icons/edit-2.png" width="35"></button> <button type="button" class="btn btn-outline-danger"><img class="" src="/icons/delete-2.png" width="35"></button></div>
        </div> <i class="fa fa-ellipsis-h"></i>
    </div>
</li>`,
    data(){
        return{

        }
    },
    methods:{}

});
app.mount('#TeacherDashboard');

//const Home = app.component('exam-list-item')

//console.log(localStorage);