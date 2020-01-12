import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/core/models/photo.model';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserService } from 'src/app/core/services/user.service';
import { AlertiflyService } from 'src/app/core/services/alertifly.service';

@Component({
  selector: 'app-photo-editing',
  templateUrl: './photo-editing.component.html',
  styleUrls: ['./photo-editing.component.css']
})
export class PhotoEditingComponent implements OnInit {

  @Input() photos : Photo[];

  uploader:FileUploader;
  hasBaseDropZoneOver = false;
  response:string;
  baseUrl = environment.apiUrl;
  currentMain : Photo;

  constructor(private readonly authService : AuthService,
    private readonly userService : UserService,
    private readonly alertyfily : AlertiflyService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() : void {
    this.uploader = new FileUploader( {
      url : this.baseUrl + "user/" +  this.authService.decodedToken.nameid + "/photos",
      authToken : 'Bearer ' + localStorage.getItem('token'),
      isHTML5 : true,
      allowedFileType : ['image'],
      removeAfterUpload : true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false;}

    this.uploader.onSuccessItem = (item,response, status, headers) => {
      if(response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id : res.id,
          url : res.url,
          dateAdded : res.dateAdded,
          description : res.description,
          isMain : res.isMain
        };

        this.photos.push(photo);
      }
    }
  }
  userMainPhoto(photo : Photo) : void {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid,photo.id).subscribe( () =>{
      this.currentMain = this.photos.filter(p => p.isMain === true)[0];
      this.currentMain.isMain = false;
      photo.isMain = true;
    }, error => {
      this.alertyfily.error("Something bad happend while setting photo as main, sorry");
    })
  }
}
