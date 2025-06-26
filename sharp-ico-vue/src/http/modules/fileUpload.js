import axios from '../axios'

export const uploadFile =  (file, sizes = null) => {
    const formData = new FormData()
    formData.append('file', file)
    if (sizes) {
        formData.append('sizes', sizes)
    }
    
    return axios({
        method: 'post',
        url: '/uploadDownload',
        data: formData,
        headers: {
            'Content-Type': 'multipart/form-data'
        },
        responseType: 'blob'
    })
}