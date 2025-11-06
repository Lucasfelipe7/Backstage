import React from 'react';
import { useEntity } from '@backstage/plugin-catalog-react';
import { useKubernetesObjects } from '@backstage/plugin-kubernetes-react';
import {
  Table,
  TableColumn,
  Progress,
  WarningPanel,
} from '@backstage/core-components';

export const KubernetesMetricsTable = () => {
  const { entity } = useEntity();
  const { kubernetesObjects, loading, error } = useKubernetesObjects(entity);

  if (loading) return <Progress />;
  if (error)
    return (
      <WarningPanel
        title="Erro ao carregar métricas"
        message={typeof error === 'string' ? error : error ?? 'Erro desconhecido'}
      />
    );


  const podMetrics =
    kubernetesObjects?.items?.flatMap(item => item.podMetrics || []) ?? [];


  const data = podMetrics.flatMap((metric: any) => {
    const podName = metric.pod?.metadata?.name ?? '-';
    const namespace = metric.pod?.metadata?.namespace ?? '-';

    return metric.containers?.map((c: any) => ({
      pod: podName,
      container: c.container ?? '-',
      namespace,
      cpuCurrent: `${(c.cpuUsage?.currentUsage * 1000).toFixed(2)} m`, // converte para millicores
      cpuLimit: `${(c.cpuUsage?.limitTotal * 1000).toFixed(2)} m`,
      memCurrent: formatBytes(c.memoryUsage?.currentUsage),
      memLimit: formatBytes(c.memoryUsage?.limitTotal),
    }));
  });

  const columns: TableColumn[] = [
    { title: 'Pod', field: 'pod' },
    { title: 'Container', field: 'container' },
    { title: 'Namespace', field: 'namespace' },
    { title: 'CPU Atual', field: 'cpuCurrent' },
    { title: 'CPU Limite', field: 'cpuLimit' },
    { title: 'Memória Atual', field: 'memCurrent' },
    { title: 'Memória Limite', field: 'memLimit' },
  ];

  return (
    <Table
      title="Métricas de Pods (CPU e Memória)"
      options={{ search: false, paging: false }}
      columns={columns}
      data={data}
    />
  );
};

// Função utilitária pra converter bytes em MB/GB
function formatBytes(bytes: string | number) {
  const value = typeof bytes === 'string' ? parseInt(bytes, 10) : bytes;
  if (!value) return '-';
  const mb = value / (1024 * 1024);
  if (mb > 1024) return `${(mb / 1024).toFixed(2)} GB`;
  return `${mb.toFixed(2)} MB`;
}
