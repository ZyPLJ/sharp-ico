import axios from '../axios'

export const uploadFile = (file, sizes = null, isMultiSize = false) => {
    const formData = new FormData()
    formData.append('file', file)
    if (sizes) {
        formData.append('sizes', sizes)
    }

    // 根据isMultiSize决定请求哪个端点
    const url = isMultiSize ? '/uploadDownload/sizes' : '/uploadDownload'

    return axios({
        method: 'post',
        url: url,
        data: formData,
        headers: {
            'Content-Type': 'multipart/form-data'
        },
        responseType: 'blob'
    })
}