﻿using CalculateFunding.Common.ApiClient.Graph.Models;
using CalculateFunding.Common.ApiClient.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CalculateFunding.Common.ApiClient.Graph
{
    public interface IGraphApiClient
    {
        Task<HttpStatusCode> UpsertCalculations(Calculation[] calculations);
        Task<HttpStatusCode> UpsertSpecifications(Specification[] specifications);
        Task<HttpStatusCode> UpsertEnums(Enum[] enums);
        Task<HttpStatusCode> DeleteCalculation(string calculationId);
        Task<HttpStatusCode> DeleteCalculations(params string[] calculationIds);
        Task<HttpStatusCode> DeleteSpecification(string specificationId);
        Task<HttpStatusCode> UpsertCalculationCalculationsRelationships(string calculationId, string[] calculationIds);
        Task<HttpStatusCode> UpsertCalculationCalculationRelationship(string calculationIdA, string calculationIdB);
        Task<HttpStatusCode> UpsertCalculationEnumRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> UpsertCalculationSpecificationRelationship(string calculationId, string specificationId);
        Task<HttpStatusCode> UpsertCalculationDataFieldsRelationships(string calculationId, string[] dataFieldIds);
        Task<HttpStatusCode> DeleteCalculationCalculationRelationship(string calculationIdA, string calculationIdB);
        Task<HttpStatusCode> DeleteCalculationCalculationRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> DeleteCalculationSpecificationRelationship(string calculationId, string specificationId);
        Task<HttpStatusCode> DeleteCalculationSpecificationRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> UpsertDataset(Dataset dataset);
        Task<HttpStatusCode> UpsertDatasets(Dataset[] datasets);
        Task<HttpStatusCode> DeleteDataset(string datasetId);
        Task<HttpStatusCode> UpsertDatasetDefinition(DatasetDefinition definition);
        Task<HttpStatusCode> UpsertDatasetDefinitions(DatasetDefinition[] definitions);
        Task<HttpStatusCode> UpsertDataField(DataField dataField);
        Task<HttpStatusCode> UpsertDataFields(DataField[] dataFields);
        Task<HttpStatusCode> DeleteDatasetDefinition(string definitionId);
        Task<HttpStatusCode> DeleteDataField(string dataFieldId);
        Task<HttpStatusCode> UpsertDataDefinitionDatasetRelationship(string definitionId, string datasetId);
        Task<HttpStatusCode> DeleteDataDefinitionDatasetRelationship(string definitionId, string datasetId);
        Task<HttpStatusCode> DeleteDatasetDataFieldRelationship(string datasetId, string fieldId);
        Task<HttpStatusCode> UpsertDatasetDataFieldRelationship(string datasetId, string fieldId);
        Task<HttpStatusCode> UpsertSpecificationDatasetRelationship(string specificationId, string datasetId);
        Task<HttpStatusCode> UpsertSpecificationDatasetRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> DeleteSpecificationDatasetRelationship(string specificationId, string datasetId);
        Task<HttpStatusCode> DeleteSpecificationDatasetRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> DeleteCalculationDataFieldRelationship(string calculationId, string fieldId);
        Task<HttpStatusCode> DeleteCalculationEnumRelationship(string calculationId, string fieldId);
        Task<HttpStatusCode> DeleteCalculationDataFieldRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> DeleteCalculationEnumRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> UpsertCalculationEnumRelationship(string calculationId, string enumId);
        Task<HttpStatusCode> UpsertCalculationDataFieldRelationship(string calculationId, string fieldId);
        Task<HttpStatusCode> UpsertCalculationDataFieldRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> UpsertFundingLines(FundingLine[] fundingLines);
        Task<HttpStatusCode> DeleteFundingLine(string fieldId);
        Task<HttpStatusCode> DeleteEnum(string enumId);
        Task<HttpStatusCode> DeleteFundingLines(params string[] fieldIds);
        Task<HttpStatusCode> DeleteEnums(string[] fieldIds);
        Task<HttpStatusCode> UpsertFundingLineCalculationRelationship(string fundingLineId, string calculationId);
        Task<HttpStatusCode> UpsertFundingLineCalculationRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> UpsertCalculationFundingLineRelationship(string calculationId, string fundingLineId);
        Task<HttpStatusCode> UpsertCalculationFundingLineRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> DeleteFundingLineCalculationRelationship(string fundingLineId, string calculationId);
        Task<HttpStatusCode> DeleteFundingLineCalculationRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> DeleteCalculationFundingLineRelationship(string calculationId, string fundingLineId);
        Task<HttpStatusCode> DeleteCalculationFundingLineRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<ApiResponse<IEnumerable<Entity<Calculation>>>> GetCircularDependencies(string specificationId);
        Task<ApiResponse<IEnumerable<Entity<Specification>>>> GetAllEntitiesRelatedToSpecification(string specificationId);
        Task<ApiResponse<IEnumerable<Entity<Calculation>>>> GetAllEntitiesRelatedToCalculation(string calculationId);
        Task<ApiResponse<IEnumerable<Entity<Enum>>>> GetAllEntitiesRelatedToEnum(string enumId);
        Task<ApiResponse<IEnumerable<Entity<Calculation>>>> GetAllEntitiesRelatedToCalculations(params string[] calculationIds);
        Task<ApiResponse<IEnumerable<Entity<DataField>>>> GetAllEntitiesRelatedToDataset(string datasetFieldId);
        Task<ApiResponse<IEnumerable<Entity<FundingLine>>>> GetAllEntitiesRelatedToFundingLine(string fundingLineId);
        Task<HttpStatusCode> UpsertDatasetDataFieldRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> UpsertDataDefinitionDatasetRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<HttpStatusCode> UpsertCalculationSpecificationRelationships(params AmendRelationshipRequestModel[] relationships);
        Task<ApiResponse<IEnumerable<Entity<FundingLine>>>> GetAllEntitiesRelatedToFundingLines(params string[] fundingLineIds);
    }
}
