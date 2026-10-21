import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { AlertService } from '../notification/alert.service';
import { ResponseDto } from 'src/app/core/models/dto/common/response.dto';

@Injectable({ providedIn: 'root' })
export class RequestService {

    constructor(private alertService: AlertService,
        private http: HttpClient) {
    }

    private responseMapper<T>() {
        return map<ResponseDto<T>, T>(result => {
            if (result === null) {
                return;
            }
            return result.data;
        })
    }

    post<T>(endpoint: string, payload: any, overrideSettings?: any): Observable<T> {
        let apiUrl = environment.baseApiUrl;

        if (overrideSettings && overrideSettings.apiUrl) {
            apiUrl = overrideSettings.apiUrl;
        }

        return this.http.post<ResponseDto<T>>(`${apiUrl}/${endpoint}`, payload)
            .pipe(this.responseMapper<T>());
    }

    get<T>(endpoint: string, overrideSettings?: any): Observable<T> {

        let baseApiUrl = environment.baseApiUrl;

        if (overrideSettings && overrideSettings.apiUrl) {
            baseApiUrl = overrideSettings.apiUrl;
        }

        return this.http.get<ResponseDto<T>>(`${baseApiUrl}/${endpoint}`)
            .pipe(this.responseMapper<T>());
    }

    localGet<T>(endpoint: string): Observable<T> {
        return this.http.get<T>(endpoint);
    }
}