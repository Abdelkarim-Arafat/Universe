//using MailKit.Search;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Universe.Core.Enums;

//namespace Universe.Application.PaymentService.Commands.CaptureOrder;

//internal class CaptureOrderCommandHandler(
//    IUnitOfWork unitOfWork,
//    IPayPalService payPalService
//    ) : IRequestHandler<CaptureOrderCommand, Result>
//{
//    private readonly IUnitOfWork _unitOfWork = unitOfWork;
//    private readonly IPayPalService _payPalService = payPalService;

//    public async Task<Result> Handle(CaptureOrderCommand request, CancellationToken cancellationToken)
//    {
//        //if (await _unitOfWork.PaymentRepository
//        //    .GetByOrderIdAsync(request.OrderId, cancellationToken) is not { } payment
//        //    ) return Result.Failure(new Error("Capture.Failed", "Failed to capture order.", StatusCodes.Status400BadRequest));

//        //var captureResult = await _payPalService.CaptureOrderAsync(request.OrderId);

//        //if (captureResult is false) return Result.Failure(new Error("Capture.captureResult", "Failed to capture order." , StatusCodes.Status400BadRequest));

//        //payment.Status = PaymentStatus.Completed;

//        //var serviceRequest = new ServiceRequest
//        //{
//        //    StudentId = payment.StudentId,
//        //    ServiceId = payment.ServiceId,
//        //    PaymentId = payment.Id,
//        //    Status = RequestStatus.Pending
//        //};

//        //await _unitOfWork.Repository<ServiceRequest>()
//        //    .AddAsync(serviceRequest, cancellationToken);

//        //await _unitOfWork.CompleteAsync(cancellationToken);

//        //return Result.Success();
//    }
//}
