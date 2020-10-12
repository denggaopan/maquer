namespace netstd Maquer.UserService.RpcServer.Thrift

service UserService { 
    ApiResult GetList(1:GetListReqDto dto) 
}

struct ApiResult{
    1: required i32 Code;
    2: required string Message;
    3: optional list<Product> Data;
}

struct GetListReqDto { 
    1: required i32 PageNumber;
    2: required i32 PageSize;
}

struct Product{
    1: required string Id;
    2: required string CategoryId;
    3: required string Name;
    4: required string SubName;
    5: required string Description;
    6: required string Tags;
    7: required string ImgUrl;
    8: required double Price;
    9: required i32 Quantity;
    10: required bool IsActive;
    11: required bool IsRecommend;
    12: required bool IsHot;
    13: required bool IsTop;
    14: required bool IsSald;
}
