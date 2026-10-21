import { ErrorDto } from './error.dto';

export interface ResponseDto<T> {
    data: T;
    error: ErrorDto
}