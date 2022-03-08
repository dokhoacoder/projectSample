import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { StudentService } from './services/student.service';
declare var $: any;
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Quản lý sinh viên';
  listStudent = [];
  dataInput: any = {};
  typeModal = 0;
  totalPage = 0;
  titleModal = "";
  captionBtnModal = "";
  tempData: any = {};
  $: any;
  constructor(private stdService: StudentService) { }

  ngOnInit() {
    this.stdService.getList("1").subscribe(res => {
      //  console.log(res);
      this.listStudent = res.data;
      this.totalPage = res.totalPage;
    });
    this.dataInput['Gender'] = 1;
  }

  counter(i: number) {
    return new Array(i);
  }

  DeleteStd(id: string) {
    console.log(id);
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.stdService.delete(id).subscribe(res => {
          Swal.fire({
            icon: 'success',
            title: res.message,
            showConfirmButton: false,
            timer: 1500
          })
          setTimeout(function () {
            window.location.reload();
          }, 2000);
        })
      }
    })
  }


  checkTypeModal(type: number) {
    if (type == 0) {
      this.dataInput = {};
      this.dataInput['Gender'] = 1;
      this.titleModal = "Add Student";
      this.captionBtnModal = "ADD";
    } else {
      this.titleModal = "Update Student";
      this.captionBtnModal = "UPDATE";
      this.stdService.getByID(type.toString()).subscribe(res => {
        this.dataInput['id'] = res.id;
        this.dataInput['Name'] = res.name;
        this.dataInput['Gender'] = res.gender;
        this.dataInput['Cmnd'] = res.cmnd;
        this.dataInput['Phone'] = res.phone;
        this.dataInput['Class'] = res.class;
        this.tempData = Object.assign({}, this.dataInput);
      });
    }
    this.typeModal = type;
  }


  ActionClick() {
    if (this.typeModal == 0) {
      this.stdService.add(this.dataInput).subscribe(res => {
        $('#stdModal').modal('toggle');
        Swal.fire({
          icon: 'success',
          title: res.message,
          showConfirmButton: false,
          timer: 1500
        })
        setTimeout(function () {
          window.location.reload();
        }, 2000);
      })
    } else if (this.typeModal != 0) {
      if (JSON.stringify(this.dataInput) !== JSON.stringify(this.tempData)) {
        this.stdService.put(this.dataInput, this.tempData['id'].toString()).subscribe(res => {
          $('#stdModal').modal('toggle');
          Swal.fire({
            icon: 'success',
            title: res.message,
            showConfirmButton: false,
            timer: 1500
          })
          setTimeout(function () {
            window.location.reload();
          }, 2000);
        })
      }else{
        Swal.fire({
          position: 'top-end',
          icon: 'error',
          title: 'Please enter your information !',
          showConfirmButton: false,
          timer: 111000
        })
      }

    }
  }
}

